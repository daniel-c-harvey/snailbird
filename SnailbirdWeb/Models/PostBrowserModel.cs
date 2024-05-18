using Core;
using RazorCore.Navigation;
using SnailbirdData.Models;

namespace SnailbirdWeb.Models
{
    public enum PostBrowserMode
    {
        Feed,
        ViewPost
    }

    public class PostBrowserFeedModel
    {
        public Page Page { get; set; }
        public IEnumerable<LiveJamPost> Posts { get; set; }

        public PostBrowserFeedModel(Page page, IEnumerable<LiveJamPost> posts)
        {
            Page = page;
            Posts = posts;
        }
    }

    public class PostBrowserViewPostModel
    {
        public LiveJamPost Post { get; set; }

        public PostBrowserViewPostModel(LiveJamPost post)
        {
            Post = post;
        }
    }

    public class PostBrowserModel : IMode<PostBrowserMode>
    {
        public PostBrowserMode CurrentMode { get; set; }
        
        public PostBrowserViewPostModel ViewPost { get; set; }
        public PostBrowserFeedModel Feed { get; set; }
        
        public PostBrowserModel(PostBrowserMode currentMode)
        {
            CurrentMode = currentMode;
            ViewPost = new PostBrowserViewPostModel(new LiveJamPost());
            Feed = new(new Page(0, 0), new List<LiveJamPost>());
        }
    }
}
