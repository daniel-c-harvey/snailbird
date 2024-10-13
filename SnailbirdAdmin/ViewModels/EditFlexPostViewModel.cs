using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexPostViewModel<TPost> : EditPostViewModelBase<TPost, EditFlexPostViewModel<TPost>>
    where TPost : FlexPost, new()
    {
        private List<PostElement> _elements = new();
        public IList<PostElement> Elements => _elements;

        public EditFlexPostViewModel(Action<TPost> onCommitPost) : base(onCommitPost) { }

        public void AddNewElement(PostElement element)
        {
            _elements.Add(element);
        }


        public void RemoveElement(PostElement element)
        {
            _elements.Remove(element);
        }

        public override EditFlexPostViewModel<TPost> LoadPost(TPost post)
        {
            _elements = post.Elements.ToList();
            return base.LoadPost(post);
        }

        public override void CommitPost()
        {
            if (Post != null)
            {
                Post.Elements = _elements;
                base.CommitPost();
            }
        }
    }
}

