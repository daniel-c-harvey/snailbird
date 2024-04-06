using DataAccess;
using SnailbirdData;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models;


namespace SnailbirdData
{
    public class DataResources<TDatabase, TDataAccess, TQueryBuilder>
        where TDataAccess : IDataAccess<TDatabase>
        where TQueryBuilder : IQueryBuilder<TDatabase>
    {
        public TDataAccess DataAccess { get; set; }
        public TQueryBuilder QueryBuilder { get; set; }

        

        public DataResources(TDataAccess dataAccess, TQueryBuilder queryBuilder)
        {
            DataAccess = dataAccess;
            QueryBuilder = queryBuilder;
        }
    }
}
