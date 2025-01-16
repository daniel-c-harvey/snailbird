using NetBlocks.Models;
using NetBlocks.Models.Environment;
using NetBlocks.Utilities.Environment;
using SnailbirdData.Models.Entities;
using SnailbirdData.Models.Post;
using MongoDB.Driver;
using DataAccess;
using SnailbirdAdminWeb.API.Managers;
using SnailbirdAdminWeb.Models;
using RazorCore.CanvasImage;
using Microsoft.JSInterop;
using SnailbirdMedia.Clients;
using SnailbirdMedia.Configs;

namespace SnailbirdAdminWeb
{
    internal static class Services
    {
        internal static bool AddServerServices(WebApplicationBuilder builder)
        {
            // Load connections and data services
            ResultContainer<Connection> connectionResults = LoadConnections(builder);
            if (connectionResults.Success && connectionResults.Value != null) 
            {
                Connection connection = connectionResults.Value;

                ResultContainer<DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>> dataResourceResults = LoadDataResources(builder, connection);
                if (!dataResourceResults.Success || dataResourceResults.Value is null) { return false; }
                DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder> dataResources = dataResourceResults.Value;

                builder.Services
                    .AddSingleton<IDataAccess<IMongoDatabase>, MongoDataAccess>(_ => dataResources.DataAccess);

                // Load model adapters
                MongoAdapter<LiveJamPost> liveJamPostAdapter;
                MongoAdapter<StudioFeedFlexPost> studioFeedFlexPostAdapter;
                MongoAdapter<LabFeedFlexPost> labFeedFlexPostAdapter;
                LoadAdapters(dataResources, out liveJamPostAdapter, out studioFeedFlexPostAdapter, out labFeedFlexPostAdapter);

                builder.Services
                    .AddSingleton<IDataAdapter<LiveJamPost>, MongoAdapter<LiveJamPost>>(_ => liveJamPostAdapter)
                    .AddSingleton<IDataAdapter<StudioFeedFlexPost>, MongoAdapter<StudioFeedFlexPost>>(_ => studioFeedFlexPostAdapter)
                    .AddSingleton<IDataAdapter<LabFeedFlexPost>, MongoAdapter<LabFeedFlexPost>>(_ => labFeedFlexPostAdapter);

                // Load external API endpoint data
                ResultContainer<ApiEndpoints> endpointResults = LoadEndpoints(builder);
                if (!endpointResults.Success || endpointResults.Value is null) { return false; }
                ApiEndpoints apiEndpoints = endpointResults.Value;

                builder.Services
                    .AddSingleton<IApiEndpoints, ApiEndpoints>(_ => apiEndpoints);
                
                // Load Post API Managers
                VaultManagerClient imageVaultClient = new(new VaultClientConfig(, "ABC123", "img"));
                FlexPostManager<StudioFeedFlexPost> studioFeedManager = new(studioFeedFlexPostAdapter);
                FlexPostManager<LabFeedFlexPost> labFeedManager = new(labFeedFlexPostAdapter);

                builder.Services
                    .AddSingleton<IPostManager<LabFeedFlexPost>, FlexPostManager<LabFeedFlexPost>>(_ => labFeedManager)
                    .AddSingleton<IPostManager<StudioFeedFlexPost>, FlexPostManager<StudioFeedFlexPost>>(_ => studioFeedManager);
            }

            // Load Connection file loader
            ConnectionStringLoader connectionStringLoader = new ConnectionStringLoader();
            ConnectionManager connectionManager = new(connectionStringLoader);

            builder.Services
                .AddSingleton<IConnectionStringLoader, ConnectionStringLoader>(_ => connectionStringLoader)
                .AddSingleton<IConnectionManager, ConnectionManager>(_ => connectionManager);

            // Load Controllers
            builder.Services
                .AddControllers();

            // Pass
            return true;
        }

        private static ResultContainer<ApiEndpoints> LoadEndpoints(WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("environment/endpoints.json", optional: false);
            ApiEndpoints? endpoints = builder.Configuration.Get<ApiEndpoints>();

            if (endpoints == null)
            {
                return ResultContainer<ApiEndpoints>.CreateFailResult("Failed to load endpoints configuration");
            }

            return new ResultContainer<ApiEndpoints>(endpoints);
        }

        private static ResultContainer<Connection> LoadConnections(WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("environment/connections.json", optional: true, reloadOnChange: true);

            Connections? connectionSecrets = builder.Configuration.Get<Connections>();

            if (connectionSecrets == null)
            {
                return ResultContainer<Connection>.CreateFailResult("Unable to load connection secrets");
            }

            Connection? connection = connectionSecrets.ConnectionStrings
                                     .FirstOrDefault(c => c.ID == connectionSecrets.ActiveConnectionID);

            if (connection == null)
            {
                return ResultContainer<Connection>.CreateFailResult("Active connection does not point to a valid connection string");
            }

            return new ResultContainer<Connection>(connection);
        }

        private static ResultContainer<DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>> LoadDataResources(WebApplicationBuilder builder, Connection connection)
        {
            ResultContainer<DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>> dataResources = new();
            try
            {
                dataResources.Value = new DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>
                (
                    new MongoDataAccess(connection.ConnectionString, connection.DatabaseName),
                    new MongoQueryBuilder()
                );
            }
            catch (Exception e)
            {
                return dataResources.Fail($"Error encountered while initializing database connection:\r\n{e.Message}");
            }

            return dataResources;
        }

        private static void LoadAdapters(DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder> dataResources,
                                         out MongoAdapter<LiveJamPost> liveJamPostAdapter,
                                         out MongoAdapter<StudioFeedFlexPost> studioFeedFlexPostAdapter,
                                         out MongoAdapter<LabFeedFlexPost> labFeedFlexPostAdapter)
        {
            liveJamPostAdapter = new(dataResources.DataAccess,
                                     dataResources.QueryBuilder,
                                     new DataSchema("studioLiveJamPost"));

            studioFeedFlexPostAdapter = new(dataResources.DataAccess,
                                            dataResources.QueryBuilder,
                                            new DataSchema("studioFeedFlexPost"));

            labFeedFlexPostAdapter = new(dataResources.DataAccess,
                                         dataResources.QueryBuilder,
                                         new DataSchema("labFeedFlexPost"));
        }
    }
}
