using DataAccess;
using NetBlocks.Models;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.API.Managers
{
    public class PostManager<TPost> : IPostManager<TPost> where TPost : Post<TPost>
    {
        private IDataAdapter<TPost> PostAdapter { get; }

        public PostManager(IDataAdapter<TPost> postAdapter)
        {
            PostAdapter = postAdapter;
        }

        public virtual async Task<ResultContainer<IEnumerable<TPost>>> GetPosts(int pageIndex, int pageSize)
        {
            return PostAdapter.GetPage(pageIndex, pageSize);
        }

        public virtual async Task<Result> InsertPost(TPost post)
        {
            return PostAdapter.Insert(post);
        }

        public virtual async Task<Result> SavePost(TPost post)
        {
            return PostAdapter.Update(post);
        }

        public virtual async Task<Result> DeletePost(TPost post)
        {
            return PostAdapter.Delete(post);
        }
    }
}
