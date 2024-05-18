using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnailbirdData.Models.Post;

namespace SnailbirdData.Providers
{
    public interface IPostProvider<TPost>
        where TPost : Post
    {
        TPost GetPost(int id);
        IEnumerable<TPost> GetRecentPosts(int pageIndex, int pageLength);
    }
}
