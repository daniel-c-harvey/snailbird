using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;
using SnailbirdWeb.Models;
using SnailbirdWeb.Messages;

namespace SnailbirdWeb.Updates
{
    public class PostBrowserUpdate<TPostModel>
    where TPostModel : Post, new()
    {
        private IDataAdapter<TPostModel> PostAdapter { get; }

        public PostBrowserUpdate(IDataAdapter<TPostModel> postAdapter)
        {
            PostAdapter = postAdapter;
        }

        public PostBrowserModel<TPostModel> Update(PostBrowserModel<TPostModel> model, PostBrowserMessage message)
        {
            switch (message.Action)
            {
                case PostBrowserAction.GetFeed:
                    var getFeedMessage = message as PostBrowserGetFeedMessage;
                    if (getFeedMessage != null) { GetFeed(model, getFeedMessage); }
                    break;
                case PostBrowserAction.ViewPost:
                    var viewPostMessage = message as PostBrowserViewPostMessage<TPostModel>;
                    if (viewPostMessage != null) { ViewPost(model, viewPostMessage); }
                    break;
                default:
                    throw new NotImplementedException();
            }

            return model;
        }

        private void GetFeed(PostBrowserModel<TPostModel> model, PostBrowserGetFeedMessage message) 
        {
            if (PostAdapter != null)
            {
                var results = PostAdapter.GetPage(message.Page.PageIndex, message.Page.PageLength);
                if (results.Success)
                {
                    model.FeedModel.Posts = results.Value;
                    model.FeedModel.Page = message.Page;
                }
            }
        }

        private void ViewPost(PostBrowserModel<TPostModel> model, PostBrowserViewPostMessage<TPostModel> message)
        {
            model.SelectedPostModel.Post = message.Post;
        }
    }
}
