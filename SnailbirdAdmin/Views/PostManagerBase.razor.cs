using Microsoft.AspNetCore.Components;
using SnailbirdAdmin.ViewModels;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Views
{
    public partial class PostManagerBase<TPost, TView, TEdit>
        where TPost : Post, new()
        where TView : PostManagerViewModel<TPost>
        where TEdit : EditPostViewModelBase<TPost, TEdit>
    {
        [Inject]
        public IDataAdapter<TPost>? PostAdapter { get; set; }
        [Parameter]
        public RenderFragment<TView>? ViewComponent { get; set; }
        [Parameter]
        public TView? ViewModel { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? AddComponent { get; set; }
        [Parameter]
        public TEdit? AddViewModel { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? EditComponent { get; set; }
        [Parameter]
        public TEdit? EditViewModel { get; set; }


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
