using NetBlocks.Models;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.API.Managers
{
    public interface IPostManager<TPost> where TPost : Post<TPost>
    {
        Result DeletePost(TPost post);
        ResultContainer<IEnumerable<TPost>> GetPosts(int pageIndex, int pageSize);
        Result InsertPost(TPost post);
        Result SavePost(TPost post);
    }
}