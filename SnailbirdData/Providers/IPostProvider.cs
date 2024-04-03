using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Providers
{
    public interface IPostProvider
    {
        TPost GetPost<TPost>(int id) where TPost : Models.Post;
        IEnumerable<TPost> GetRecentPosts<TPost>(int pageIndex, int pageLength) where TPost : Models.Post;
    }
}
