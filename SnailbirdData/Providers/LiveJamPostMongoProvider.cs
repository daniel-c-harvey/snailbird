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
    public class LiveJamPostMongoProvider : PostDatabaseProvider<MongoAdapter<LiveJamPost>, LiveJamPost>
    {
        public LiveJamPostMongoProvider(MongoAdapter<LiveJamPost> dataAdapter) 
        : base(dataAdapter) { }
    }
}
