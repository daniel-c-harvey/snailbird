using Microsoft.AspNetCore.Components;
using RazorCore;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Views
{
    public partial class EditPost<TPost>
        where TPost : Post, new()
    {
        [Parameter]
        public Action<TPost> OnCommitPost { get; set; }

        [Parameter]
        public TPost Post { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; } = null;        

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected virtual void CommitPost()
        {
            OnCommitPost(Post);
        }
    }
}