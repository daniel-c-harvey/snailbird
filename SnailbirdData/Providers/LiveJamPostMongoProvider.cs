using DataAccess;
using SnailbirdData.Models.Post;

namespace SnailbirdData.Providers
{
    public class LiveJamPostMongoProvider : PostDatabaseProvider<MongoAdapter<LiveJamPost>, LiveJamPost>
    {
        public LiveJamPostMongoProvider(MongoAdapter<LiveJamPost> dataAdapter) 
        : base(dataAdapter) { }
    }
}
