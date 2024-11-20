using DataAccess;
using MongoDB.Driver;
using NetBlocks.Models.Environment;
using SnailbirdData.Adapters;
using SnailbirdData.Models.Entities;
using SnailbirdData.Models.Post;

namespace SnailbirdUtility.Converters
{
    public static class LiveJamPostToFlexPostConverter
    {
        public static void ConvertLiveJamPostToFlexPosts(string databaseName)
        {
            var dataAccess = new MongoDataAccess
            (
                ConnectionStringTools.LoadFromFile("../../../.secrets/connections.json", "mongodb-snailbird-admin").ConnectionString,
                databaseName
            );

            var queryBuilder = new MongoQueryBuilder();

            var dataResources = new DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>
            (
                dataAccess,
                queryBuilder
            );

            MongoAdapter<LiveJamPost> oldPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("posts"));
            MongoAdapter<LiveJamPost> newLiveJamPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioLiveJamPost"));
            MongoAdapter<StudioFeedFlexPost> studioPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioFeedFlexPost"));
            MongoAdapter<LabFeedFlexPost> labPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("labFeedFlexPost"));

            IEnumerable<LiveJamPost> livejamposts = oldPostAdapter.GetPage(0, 10).Value.ToList();

            foreach (LiveJamPost post in livejamposts)
            {
                //flexPostAdapter.Insert(post.AdaptFlex());
                newLiveJamPostAdapter.Insert(post);
            }
        }
    }
}
