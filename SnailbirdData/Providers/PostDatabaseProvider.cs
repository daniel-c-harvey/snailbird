using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnailbirdData.Models;

namespace SnailbirdData.Providers
{
    public class PostDatabaseProvider : IPostProvider
    {
        private string _connectionString; // move all connection specific code to the core DataAccess for mongo connections under an abstract connection object with generic ExecRange Exec
        // private Object connection;
        public PostDatabaseProvider(string connectionString)
        {
            _connectionString = connectionString;

        }

        public TPost GetPost<TPost>(int id) where TPost : Post
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TPost> GetRecentPosts<TPost>(int pageIndex, int pageLength) where TPost : Post
        {
            throw new NotImplementedException();
        }
    }
}