using Core;
using DataAccess;
using RazorCore.Navigation;
using SnailbirdData.Models.Post;
using SnailbirdWeb.Messages;
using SnailbirdWeb.Models;
using SnailbirdWeb.Updates;

namespace SnailbirdWeb.ViewModels
{
    public class PostBrowserViewModel<TPostModel> : INavigable<PostBrowserMode>
    where TPostModel : Post<TPostModel>, new()
    {
        public PostBrowserModel<TPostModel> Model { get; set; }
        private PostBrowserUpdate<TPostModel> Update;

        public PostBrowserViewModel(IDataAdapter<TPostModel> postAdapter)
        {
            Model = new();
            Update = new(postAdapter);
            Navigator = new Navigator<PostBrowserMode, PostBrowserModel<TPostModel>>(Model);
            
            Update.Update(Model, new PostBrowserGetFeedMessage(new Page(0, 25)), Navigator);
        }

        public void ViewPost(TPostModel post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostBrowserViewPostMessage<TPostModel>(post), Navigator);
            }
        }

        public INavigator<PostBrowserMode> Navigator { get; protected set; }
    }
}
