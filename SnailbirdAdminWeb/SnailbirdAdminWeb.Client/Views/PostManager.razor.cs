using Microsoft.AspNetCore.Components;
using SnailbirdAdminWeb.Client.Updates;
using SnailbirdData.Models.Post;
using SnailbirdAdminWeb.Client.ViewModels;

namespace SnailbirdAdminWeb.Client.Views
{
    public partial class PostManager<TPost, TEdit, TUpdate>
        where TPost : Post<TPost>, new()
        where TEdit : EditPostViewModelBase<TPost, TEdit>
        where TUpdate : PostManagerUpdate<TPost>
    {
        [Parameter]
        public PostManagerViewModel<TPost, TEdit, TUpdate>? ViewModel { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? AddComponent { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? EditComponent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (ViewModel != null) ViewModel.Navigator.ModeChanged += (_) => ModeChanged();
        }

        private void ModeChanged()
        {
            StateHasChanged();
        }
    }
}
