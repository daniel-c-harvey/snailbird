using DataAccess;
using SnailbirdData.Models.Post;
using SnailbirdWeb.Models;
using SnailbirdWeb.Messages;
using RazorCore.Navigation;

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

        public PostBrowserModel<TPostModel> Update(PostBrowserModel<TPostModel> model, 
                                                   PostBrowserMessage message, 
                                                   INavigator<PostBrowserMode> navigator)
        {
            switch (message.Action)
            {
                case PostBrowserAction.GetFeed:
                    var getFeedMessage = message as PostBrowserGetFeedMessage;
                    if (getFeedMessage != null) { GetFeed(model, getFeedMessage, navigator); }
                    break;
                case PostBrowserAction.ViewPost:
                    var viewPostMessage = message as PostBrowserViewPostMessage<TPostModel>;
                    if (viewPostMessage != null) { ViewPost(model, viewPostMessage, navigator); }
                    break;
                default:
                    throw new NotImplementedException();
            }

            return model;
        }

        private void GetFeed(PostBrowserModel<TPostModel> model, 
                             PostBrowserGetFeedMessage message,
                             INavigator<PostBrowserMode> navigator) 
        {
            if (PostAdapter != null)
            {
                var results = PostAdapter.GetPage(message.Page.PageIndex, message.Page.PageLength);
                if (results.Success)
                {
                    model.FeedModel.Posts = results.Value;
                    model.FeedModel.Page = message.Page;
                    RegisterForwardNavigation(navigator, PostBrowserMode.Feed);
                }
            }
        }

        private void ViewPost(PostBrowserModel<TPostModel> model, 
                              PostBrowserViewPostMessage<TPostModel> message, 
                              INavigator<PostBrowserMode> navigator)
        {
            model.SelectedPostModel.Post = message.Post;
            RegisterForwardNavigation(navigator, PostBrowserMode.ViewPost);
        }

        private void RegisterForwardNavigation(INavigator<PostBrowserMode> navigator, PostBrowserMode newMode)
        {
            navigator.NavigateForward(newMode);
        }

        protected void RegisterBackNavigation(INavigator<PostBrowserMode> navigator)
        {
            navigator.NavigateBack();
        }
    }
}
