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

    public class PostManagerModel : IMode<PostManagerMode>
    {
        public IEnumerable<LiveJamPost> Posts { get; set; }
        public LiveJamPost Post { get; set; }
        public PostManagerMode CurrentMode { get; set; }

        public PostManagerModel(PostManagerMode currentMode)
        {
            Posts = new List<LiveJamPost>();
            Post = new LiveJamPost();
            CurrentMode = currentMode;
        }

        public PostManagerModel(IEnumerable<LiveJamPost> posts, LiveJamPost post, PostManagerMode currentMode)
        {
            Posts = posts;
            Post = post;
            CurrentMode = currentMode;
        }
    }
}
