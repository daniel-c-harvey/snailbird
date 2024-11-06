using Microsoft.AspNetCore.Components;
using Core;
using DataAccess;
using RazorCore.Navigation;
using SnailbirdData.Models.Post;
using SnailbirdWeb.Models;
using SnailbirdWeb.Updates;
using SnailbirdWeb.Messages;
using SnailbirdWeb.ViewModels;

namespace SnailbirdWeb.Views
{
    public partial class PostBrowser<TPostModel>
    where TPostModel : Post<TPostModel>, new()
    {
        [Inject]
        public IDataAdapter<TPostModel>? PostAdapter { get; set; }

        [Parameter]
        public PostBrowserViewModel<TPostModel>? ViewModel { get; set; }

        protected override void OnInitialized()
        {
            if (ViewModel != null) ViewModel.Navigator.ModeChanged += ModeChanged;
            base.OnInitialized();
        }

        private void ModeChanged(ModeChangeEventArgs<PostBrowserMode> args)
        {
            StateHasChanged();
        }
    }
}
