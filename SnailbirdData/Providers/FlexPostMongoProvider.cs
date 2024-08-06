using DataAccess;
using MongoDB.Driver;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Providers
{
    public class FlexPostMongoProvider : PostDatabaseProvider<MongoAdapter<FlexPost>, FlexPost>
    {
        public FlexPostMongoProvider(MongoAdapter<FlexPost> dataAdapter) 
        : base(dataAdapter) { }
    }
}
