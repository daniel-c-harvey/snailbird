using Microsoft.AspNetCore.Components;
using DataAccess;
using SnailbirdData.Models.Post;
using SnailbirdAdminWeb.Client.ViewModels;

namespace SnailbirdAdminWeb.Client.Views
{
    public partial class PostManagerBase<TPost, TEdit, TView>
        where TPost : Post<TPost>, new()
        where TEdit : EditPostViewModelBase<TPost, TEdit>
        where TView : PostManagerViewModel<TPost, TEdit>
    {
        [Parameter]
        public TView? ViewModel { get; set; }
        [Parameter]
        public RenderFragment<TView>? ViewComponent { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? AddComponent { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? EditComponent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (ViewModel != null)
            {
                ViewModel.Navigator.ModeChanged += (_) => ModeChanged();
            }
        }

        public void ModeChanged()
        {
            StateHasChanged();
        }
    }
}
