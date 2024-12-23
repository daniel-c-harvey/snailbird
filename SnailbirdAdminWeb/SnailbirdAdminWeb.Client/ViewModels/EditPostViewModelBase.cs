using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public abstract class EditPostViewModelBase<TPost, TSelf>
        where TPost : Post<TPost>, new()
        where TSelf : EditPostViewModelBase<TPost, TSelf>
    {
        public TPost? Post { get; set; }
        public Action<TPost> OnCommitPost { get; set; }

        public EditPostViewModelBase(Action<TPost> onCommitPost)
        {
            OnCommitPost = onCommitPost;
        }

        public virtual TSelf LoadPost(TPost post)
        {
            Post = post;
            return (TSelf)this;
        }

        public void CommitPost()
        {
            if (OnCommitPost != null && Post != null)
            {
                OnCommitPost(Post);
            }
        }
    }
}
