using SnailbirdData.Models;
using SnailbirdData.Providers;
using SnailbirdWeb.Models;
using SnailbirdWeb.Messages;
using SnailbirdData.DataAdapters;

namespace SnailbirdWeb.Updates
{
    public class PostBrowserUpdate
    {
        private IDataAdapter<LiveJamPost> PostAdapter { get; }

        public PostBrowserUpdate(IDataAdapter<LiveJamPost> postAdapter)
        {
            PostAdapter = postAdapter;
        }

        public PostBrowserModel Update(PostBrowserModel model, PostBrowserMessage message)
        {
            switch (message.Action)
            {
                case PostBrowserAction.GetFeed:
                    var getFeedMessage = message as PostBrowserGetFeedMessage;
                    if (getFeedMessage != null) { GetFeed(model, getFeedMessage); }
                    break;
                default:
                    throw new NotImplementedException();
            }

            return model;
        }

        private void GetFeed(PostBrowserModel model, PostBrowserGetFeedMessage message) 
        {
            if (PostAdapter != null)
            {
                var results = PostAdapter.GetPage(message.Page.PageIndex, message.Page.PageLength);
                if (results.Success)
                {
                    model.Feed.Posts = results.Value;
                    model.Feed.Page = message.Page;
                }
            }
        }
    }
}
