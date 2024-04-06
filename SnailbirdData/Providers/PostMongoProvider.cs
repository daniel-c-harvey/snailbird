using DataAccess;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Providers
{
    public class PostMongoProvider : PostDatabaseProvider<MongoDataAccess, IMongoDatabase, MongoQueryBuilder>
    {
        public PostMongoProvider(string connectionString, string databaseName) 
        : base(connectionString, databaseName) { }
    }
}
