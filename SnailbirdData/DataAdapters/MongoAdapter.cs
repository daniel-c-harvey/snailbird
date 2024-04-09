using DataAccess;
using MongoDB.Driver;

namespace SnailbirdData.DataAdapters
{
    public class MongoAdapter<TModel> : DataAdapter<IMongoDatabase, MongoDataAccess, MongoQueryBuilder, TModel>
        where TModel : IModel
    {
        public MongoAdapter(MongoDataAccess dataAccess, MongoQueryBuilder queryBuilder, DataSchema schema) 
        : base(dataAccess, queryBuilder, schema) { }
    }
}
