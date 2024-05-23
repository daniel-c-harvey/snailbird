using Microsoft.AspNetCore.Components;
using Core;
using RazorCore.Navigation;
using SnailbirdData.Models.Post;
using SnailbirdData.DataAdapters;
using SnailbirdWeb.Models;
using SnailbirdWeb.Updates;
using SnailbirdWeb.Messages;

namespace SnailbirdWeb.Views
{
    public partial class PostBrowser : INavigable<PostBrowserMode>
    {
        #region "Members"
        [Inject]
        public IDataAdapter<LiveJamPost> PostAdapter { get; set; }

        private PostBrowserModel? model;
        private PostBrowserUpdate? update;
        private void InitModel()
        {
            model = new(PostBrowserMode.Feed);
            update = new(PostAdapter);
            update.Update(model, new PostBrowserGetFeedMessage(new Page(0, 25)));
        }
        #endregion

        #region "Event Handlers"
        protected override void OnInitialized()
        {
            InitModel();
            InitNavigation();
        }
        #endregion

        #region "View Model"
        private void ViewPost(LiveJamPost post)
        {
            if (update != null && model != null) 
            {
                BeforeModeChange();
                update.Update(model, new PostBrowserViewPostMessage(post));
            }
        }
        #endregion

        #region "INavigable"
        private void InitNavigation()
        {
            Navigator = new Navigator<PostBrowserMode, PostBrowserModel>(model);
            Navigator.ModeChanging += (_) => ModeChanged();
        }

        public INavigator<PostBrowserMode> Navigator { get; private set; }
        private PostBrowserMode CurrentMode => model.CurrentMode;
        private void BeforeModeChange()
        {
            if (model != null)
            {
                Navigator.OnForward();
            }
        }

        public void ModeChanged()
        {
            StateHasChanged();
        }
        #endregion
    }
}
