using DataAccess;
using SnailbirdData.Models.Post;

namespace SnailbirdData.Providers
{
    public class FlexPostMongoProvider : PostDatabaseProvider<MongoAdapter<FlexPost>, FlexPost>
    {
        public FlexPostMongoProvider(MongoAdapter<FlexPost> dataAdapter) 
        : base(dataAdapter) { }
    }
}
