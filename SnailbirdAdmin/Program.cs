using SnailbirdAdmin.Components;
using DataAccess;
using SnailbirdData.Models.Post;
using SnailbirdData.Models.Entities;
using SnailbirdData.Providers;
using MongoDB.Driver;
using SnailbirdWeb.Models;

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

            AddGlobalServices(builder);

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

        private static void AddGlobalServices(WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
            ConnectionSecrets? connectionSecrets = builder.Configuration.Get<ConnectionSecrets>();

            if (connectionSecrets == null)
            {
                Console.WriteLine("Unable to load connection secrets");
                return;
            }

            Connection? connection = connectionSecrets.Connections?
                                     .FirstOrDefault(c => c.ConnectionName.Equals(connectionSecrets.ActiveConnectionName,
                                                                                  StringComparison.OrdinalIgnoreCase));

            if (connection == null)
            {
                Console.WriteLine("Active connection does not point to a valid connection string");
                return;
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
                return;
            }

            MongoAdapter<LiveJamPost> liveJamPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioLiveJamPost"));
            MongoAdapter<StudioFeedFlexPost> studioFeedFlexPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioFeedFlexPost"));
            MongoAdapter<LabFeedFlexPost> labFeedFlexPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("labFeedFlexPost"));

            builder.Services
            .AddSingleton<IDataAccess<IMongoDatabase>,MongoDataAccess>(_ => dataResources.DataAccess)
            .AddSingleton<IDataAdapter<LiveJamPost>, MongoAdapter<LiveJamPost>>(_ => liveJamPostAdapter)
            .AddSingleton<IDataAdapter<StudioFeedFlexPost>, MongoAdapter<StudioFeedFlexPost>>(_ => studioFeedFlexPostAdapter)
            .AddSingleton<IDataAdapter<LabFeedFlexPost>, MongoAdapter<LabFeedFlexPost>>(_ => labFeedFlexPostAdapter)
            .AddSingleton<IPostProvider<LiveJamPost>, LiveJamPostMongoProvider>(provider => new LiveJamPostMongoProvider(liveJamPostAdapter));

        }
    }
}
