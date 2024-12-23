using NetBlocks.Models;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.API
{
    public interface IPostManagerClient<TPost> where TPost : Post<TPost>
    {
        Task<ResultContainer<IEnumerable<TPost>>> GetPage(int page, int size);
        Task<Result> Update(TPost post);
        Task<Result> Insert(TPost post);
    }
}