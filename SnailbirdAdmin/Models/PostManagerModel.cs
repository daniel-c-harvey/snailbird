using RazorCore.Navigation;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Models
{
    public enum PostManagerMode
    {
        View,
        Add,
        Edit
    }

    public class PostManagerModel<TPost> : IMode<PostManagerMode>
        where TPost : Post, new()
    {
        public IEnumerable<TPost> Posts { get; set; }
        public TPost Post { get; set; }
        public PostManagerMode CurrentMode { get; set; }

        public PostManagerModel(PostManagerMode currentMode)
        {
            Posts = new List<TPost>();
            Post = new TPost();
            CurrentMode = currentMode;
        }

        public PostManagerModel(IEnumerable<TPost> posts, TPost post, PostManagerMode currentMode)
        {
            Posts = posts;
            Post = post;
            CurrentMode = currentMode;
        }

        public TPost CurrentPost()
        {
            return Post;
        }
    }
}
