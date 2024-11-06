using Core;
using RazorCore.Navigation;
using SnailbirdData.Models.Post;

namespace SnailbirdWeb.Models
{
    public class PostBrowserMode : Enumeration<PostBrowserMode>
    {
        public static PostBrowserMode Feed = new(1, nameof(Feed));
        public static PostBrowserMode ViewPost = new(2, nameof(ViewPost));

        public PostBrowserMode(int id, string name) : base(id, name) { }
    }

    public class PostBrowserFeedModel<TPostModel>
    where TPostModel : Post<TPostModel>, new()
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
    where TPostModel : Post<TPostModel>, new()
    {
        public TPostModel Post { get; set; }

        public PostBrowserViewPostModel(TPostModel post)
        {
            Post = post;
        }
    }

    public class PostBrowserModel<TPostModel> : IMode<PostBrowserMode>
    where TPostModel : Post<TPostModel>, new()
    {
        public PostBrowserMode CurrentMode { get; set; }
        
        public PostBrowserViewPostModel<TPostModel> SelectedPostModel { get; set; }
        public PostBrowserFeedModel<TPostModel> FeedModel { get; set; }
        
        public PostBrowserModel()
        {
            SelectedPostModel = new PostBrowserViewPostModel<TPostModel>(new TPostModel());
            FeedModel = new(new Page(0, 0), new List<TPostModel>());
        }
    }
}
