using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using SnailbirdData;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;

namespace SnailbirdData.Providers
{
    public abstract class PostDatabaseProvider<TDataAdapter, TPost> : IPostProvider<TPost>
        where TDataAdapter : IDataAdapter<TPost>
        where TPost : Post
    {
        TDataAdapter DataAdapter { get; set; }

        public PostDatabaseProvider(TDataAdapter dataAdapter)
        {
            DataAdapter = dataAdapter;
        }

        public TPost GetPost(int id)
        {
            var results = DataAdapter.GetByID(id);
            return results.Value;
        }

        public IEnumerable<TPost> GetRecentPosts(int pageIndex, int pageLength)
        {
            var results = DataAdapter.GetPage(pageIndex, pageLength);
            return results.Value;
        }
    }
}