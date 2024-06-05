using Microsoft.AspNetCore.Components;
using SnailbirdAdmin.ViewModels;
using SnailbirdAdmin.Views;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;
using System.Diagnostics.Contracts;

namespace SnailbirdAdmin.Views
{
    public partial class PostManager<TPost, TEdit>
        where TPost : Post, new()
        where TEdit : EditPostViewModelBase<TPost, TEdit>
    {
        [Inject]
        public IDataAdapter<TPost>? PostAdapter { get; set; }

        [Parameter]
        public PostManagerViewModel<TPost>? ViewModel { get; set; }
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
