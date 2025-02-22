using SnailbirdAdmin.Components;
using DataAccess;
using SnailbirdData.Models.Post;
using SnailbirdData.Models.Entities;
using SnailbirdData.Providers;
using MongoDB.Driver;
using NetBlocks.Models.Environment;

namespace SnailbirdAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            if (!AddGlobalServices(builder))
            {
                return; // Abort
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }

        private static bool AddGlobalServices(WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("environment/connections.json", optional: true, reloadOnChange: true);
            
            Connections? connectionSecrets = builder.Configuration.Get<Connections>();

            if (connectionSecrets == null)
            {
                Console.WriteLine("Unable to load connection secrets");
                return false;
            }

            Connection? connection = connectionSecrets.ConnectionStrings
                                     .FirstOrDefault(c => c.ID == connectionSecrets.ActiveConnectionID);

            if (connection == null)
            {
                Console.WriteLine("Active connection does not point to a valid connection string");
                return false;
            }

            DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder> dataResources;
            try
            {
                dataResources = new DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>
                (
                    new MongoDataAccess(connection.ConnectionString, connection.DatabaseName),
                    new MongoQueryBuilder()
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error encountered while initializing database connection:\r\n{e.Message}");
                return false;
            }

            MongoAdapter<LiveJamPost> liveJamPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioLiveJamPost"));
            MongoAdapter<StudioFeedFlexPost> studioFeedFlexPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioFeedFlexPost"));
            MongoAdapter<LabFeedFlexPost> labFeedFlexPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("labFeedFlexPost"));

            builder.Configuration.AddJsonFile("environment/endpoints.json", optional: false);
            Endpoints? endpoints = builder.Configuration.Get<Endpoints>();

            if (endpoints == null)
            {
                Console.WriteLine("Failed to load endpoints configuration");
                return false;
            }

            builder.Services
            .AddSingleton<IDataAccess<IMongoDatabase>, MongoDataAccess>(_ => dataResources.DataAccess)
            .AddSingleton<IDataAdapter<LiveJamPost>, MongoAdapter<LiveJamPost>>(_ => liveJamPostAdapter)
            .AddSingleton<IDataAdapter<StudioFeedFlexPost>, MongoAdapter<StudioFeedFlexPost>>(_ => studioFeedFlexPostAdapter)
            .AddSingleton<IDataAdapter<LabFeedFlexPost>, MongoAdapter<LabFeedFlexPost>>(_ => labFeedFlexPostAdapter)
            .AddSingleton<IPostProvider<LiveJamPost>, LiveJamPostMongoProvider>(provider => new LiveJamPostMongoProvider(liveJamPostAdapter))
            .AddSingleton<IEndpoints, Endpoints>(_ => endpoints);

            return true;
        }
    }
}
