using Microsoft.AspNetCore.Components;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditPostViewModel<TPost> : EditPostViewModelBase<TPost, EditPostViewModel<TPost>>
        where TPost : Post, new()
    {
        public EditPostViewModel(Action<TPost> onCommitPost)
        : base(onCommitPost)
        { }
    }
}
