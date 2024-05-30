using Microsoft.AspNetCore.Components;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditPostViewModel<TPost>
        where TPost : Post, new()
    {
        [Parameter]
        public Action<TPost>? OnCommitPost { get; set; }

        [Parameter]
        public TPost? Post { get; set; }

        public virtual void CommitPost()
        {
            if (OnCommitPost != null && Post != null)
            {
                OnCommitPost(Post);
            }
        }
    }
}
