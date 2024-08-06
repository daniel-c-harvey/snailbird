using Core;
using RazorCore.Navigation;
using SnailbirdData.Models.Post;

namespace SnailbirdWeb.Models
{
    public enum PostBrowserMode
    {
        Feed,
        ViewPost
    }

    public class PostBrowserFeedModel<TPostModel>
    where TPostModel : Post, new()
    {
        public Page Page { get; set; }
        public IEnumerable<TPostModel> Posts { get; set; }

        public PostBrowserFeedModel(Page page, IEnumerable<TPostModel> posts)
        {
            Page = page;
            Posts = posts;
        }
    }

    public class PostBrowserViewPostModel<TPostModel>
    where TPostModel : Post, new()
    {
        public TPostModel Post { get; set; }

        public PostBrowserViewPostModel(TPostModel post)
        {
            Post = post;
        }
    }

    public class PostBrowserModel<TPostModel> : IMode<PostBrowserMode>
    where TPostModel : Post, new()
    {
        public PostBrowserMode CurrentMode { get; set; }
        
        public PostBrowserViewPostModel<TPostModel> SelectedPostModel { get; set; }
        public PostBrowserFeedModel<TPostModel> FeedModel { get; set; }
        
        public PostBrowserModel(PostBrowserMode currentMode)
        {
            CurrentMode = currentMode;
            SelectedPostModel = new PostBrowserViewPostModel<TPostModel>(new TPostModel());
            FeedModel = new(new Page(0, 0), new List<TPostModel>());
        }
    }
}
