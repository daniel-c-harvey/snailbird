using DataAccess;
using MongoDB.Driver;
using NetBlocks.Models.Environment;
using SnailbirdAdminWeb.Components;
using SnailbirdAdminWeb.Models;
using SnailbirdData.Models.Entities;
using SnailbirdData.Models.Post;
using SnailbirdData.Providers;

namespace SnailbirdAdminWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            if (!AddServerServices(builder))
            {
                return; // Abort
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.MapControllers();

            app.Run();
        }

        private static bool AddServerServices(WebApplicationBuilder builder)
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
            MongoAdapter<StudioFeedFlexPost> studioFeedFlexPostAdapter = new(dataResources.DataAccess, 
                                                                             dataResources.QueryBuilder, 
                                                                             new DataSchema("studioFeedFlexPost"));
            MongoAdapter<LabFeedFlexPost> labFeedFlexPostAdapter = new(dataResources.DataAccess, 
                                                                       dataResources.QueryBuilder, 
                                                                       new DataSchema("labFeedFlexPost"));

            API.Managers.PostManager<StudioFeedFlexPost> studioFeedManager = new(studioFeedFlexPostAdapter);
            API.Managers.PostManager<LabFeedFlexPost> labFeedManager = new(labFeedFlexPostAdapter);


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
            .AddSingleton<IPostProvider<LiveJamPost>, LiveJamPostMongoProvider>(_ => new LiveJamPostMongoProvider(liveJamPostAdapter))
            .AddSingleton<IEndpoints, Endpoints>(_ => endpoints)
            .AddSingleton<API.Managers.IPostManager<LabFeedFlexPost>, API.Managers.PostManager<LabFeedFlexPost>>(_ => labFeedManager)
            .AddSingleton<API.Managers.IPostManager<StudioFeedFlexPost>, API.Managers.PostManager<StudioFeedFlexPost>>(_ => studioFeedManager)
            .AddControllers();

            return true;
        }
    }
}
