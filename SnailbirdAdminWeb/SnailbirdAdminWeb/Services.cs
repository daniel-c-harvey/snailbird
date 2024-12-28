using NetBlocks.Models;
using NetBlocks.Models.Environment;
using NetBlocks.Utilities.Environment;
using SnailbirdData.Models.Entities;
using SnailbirdData.Models.Post;
using MongoDB.Driver;
using DataAccess;
using SnailbirdAdminWeb.API.Managers;
using SnailbirdAdminWeb.Models;

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

                // Load Post API Managers
                PostManager<StudioFeedFlexPost> studioFeedManager = new(studioFeedFlexPostAdapter);
                PostManager<LabFeedFlexPost> labFeedManager = new(labFeedFlexPostAdapter);

                builder.Services
                    .AddSingleton<IPostManager<LabFeedFlexPost>, PostManager<LabFeedFlexPost>>(_ => labFeedManager)
                    .AddSingleton<IPostManager<StudioFeedFlexPost>, PostManager<StudioFeedFlexPost>>(_ => studioFeedManager);
            }

            // Load Connection file loader
            ConnectionStringLoader connectionStringLoader = new ConnectionStringLoader();
            ConnectionManager connectionManager = new(connectionStringLoader);

            builder.Services
                .AddSingleton<IConnectionStringLoader, ConnectionStringLoader>(_ => connectionStringLoader)
                .AddSingleton<IConnectionManager, ConnectionManager>(_ => connectionManager);

            // Load external API endpoint data
            ResultContainer<Endpoints> endpointResults = LoadEndpoints(builder);
            if (!endpointResults.Success || endpointResults.Value is null) { return false; }
            Endpoints endpoints = endpointResults.Value;

            builder.Services
                .AddSingleton<IEndpoints, Endpoints>(_ => endpoints);
            
            // Load Controllers
            builder.Services
                .AddControllers();

            // Pass
            return true;
        }

        private static ResultContainer<Endpoints> LoadEndpoints(WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("environment/endpoints.json", optional: false);
            Endpoints? endpoints = builder.Configuration.Get<Endpoints>();

            if (endpoints == null)
            {
                return ResultContainer<Endpoints>.CreateFailResult("Failed to load endpoints configuration");
            }

            return new ResultContainer<Endpoints>(endpoints);
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
