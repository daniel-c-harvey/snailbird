using DataAccess;
using MongoDB.Driver;
using SnailbirdData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.DataAdapters
{
    public class MongoAdapter<TModel> : DataAdapter<IMongoDatabase, MongoDataAccess, MongoQueryBuilder, TModel>
        where TModel : class
    {
        public MongoAdapter(MongoDataAccess dataAccess, MongoQueryBuilder queryBuilder, DataSchema schema) 
        : base(dataAccess, queryBuilder, schema) { }
    }
}
