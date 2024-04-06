using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Providers
{
    public interface IPostProvider<TPost>
        where TPost : Models.Post
    {
        TPost GetPost(int id);
        IEnumerable<TPost> GetRecentPosts(int pageIndex, int pageLength);
    }
}
