using Microsoft.AspNetCore.Components;
using SnailbirdData.Models.Post;
using SnailbirdAdminWeb.Client.ViewModels;

namespace SnailbirdAdminWeb.Client.Views
{
    public partial class PostManager<TPost, TEdit>
        where TPost : Post<TPost>, new()
        where TEdit : EditPostViewModelBase<TPost, TEdit>
    {
        [Parameter]
        public PostManagerViewModel<TPost, TEdit>? ViewModel { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? AddComponent { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? EditComponent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (ViewModel != null) ViewModel.Navigator.ModeChanged += (_) => ModeChanged();
        }

        public void ModeChanged()
        {
            StateHasChanged();
        }
    }
}
