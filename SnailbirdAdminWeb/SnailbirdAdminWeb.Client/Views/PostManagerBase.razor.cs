﻿using Microsoft.AspNetCore.Components;
using DataAccess;
using SnailbirdData.Models.Post;
using SnailbirdAdminWeb.Client.ViewModels;

namespace SnailbirdAdminWeb.Client.Views
{
    public partial class PostManagerBase<TPost, TView, TEdit>
        where TPost : Post<TPost>, new()
        where TView : PostManagerViewModel<TPost>
        where TEdit : EditPostViewModelBase<TPost, TEdit>
    {
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
