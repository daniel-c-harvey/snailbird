using NetBlocks.Models;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.API.Managers
{
    public interface IPostManager<TPost> where TPost : Post<TPost>
    {
        Task<Result> DeletePost(TPost post);
        Task<ResultContainer<IEnumerable<TPost>>> GetPosts(int pageIndex, int pageSize);
        Task<Result> InsertPost(TPost post);
        Task<Result> SavePost(TPost post);
    }
}