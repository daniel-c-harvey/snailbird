using DataAccess;
using NetBlocks.Models;
using RazorCore.Navigation;
using SnailbirdAdminWeb.Client.Messages;
using SnailbirdAdminWeb.Client.Models;
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

        public ResultContainer<IEnumerable<TPost>> GetPosts(int pageIndex, int pageSize)
        {
            return PostAdapter.GetPage(pageIndex, pageSize);
        }

        public Result InsertPost(TPost post)
        {
            return PostAdapter.Insert(post);
        }

        public Result SavePost(TPost post)
        {
            return PostAdapter.Update(post);
        }

        public Result DeletePost(TPost post)
        {
            return PostAdapter.Delete(post);
        }
    }
}
