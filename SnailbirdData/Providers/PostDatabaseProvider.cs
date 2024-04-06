using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using SnailbirdData.Models;

namespace SnailbirdData.Providers
{
    public abstract class PostDatabaseProvider<TDataAccess, TDatabase, TQueryBuilder> : IPostProvider 
        where TDataAccess : IDataAccess<TDatabase>, new()
        where TDatabase : class
        where TQueryBuilder : IQueryBuilder<TDatabase>, new()
    {
        protected TDataAccess DataAccess;
        protected TQueryBuilder QueryBuilder;

        public PostDatabaseProvider(string connectionString, string databaseName)
        {
            DataAccess = new TDataAccess();
            QueryBuilder = new TQueryBuilder();
            DataAccess.OpenConnection(connectionString, databaseName);
        }

        public TPost GetPost<TPost>(int id) where TPost : Post
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TPost> GetRecentPosts<TPost>(int pageIndex, int pageLength) where TPost : Post
        {
            return DataAccess.ExecQuery<TPost>(QueryBuilder.BuildQuery<TPost>("posts"));
        }
    }
}