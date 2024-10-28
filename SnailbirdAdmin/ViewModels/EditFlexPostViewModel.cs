using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexPostViewModel<TPost> : EditPostViewModelBase<TPost, EditFlexPostViewModel<TPost>>
    where TPost : FlexPost, new()
    {
        private List<EditFlexElementViewModel> _elements = new();
        public IList<EditFlexElementViewModel> Elements => _elements;

        public EditFlexPostViewModel(Action<TPost> onCommitPost) : base(onCommitPost) { }

        public void AddNewElement(EditFlexElementViewModel element)
        {
            _elements.Add(element);
        }


        public void RemoveElement(EditFlexElementViewModel element)
        {
            _elements.Remove(element);
        }

        public override EditFlexPostViewModel<TPost> LoadPost(TPost post)
        {
            _elements = post.Elements.Select(e => new EditFlexElementViewModel() { Element = e }).ToList();
            return base.LoadPost(post);
        }

        public override void CommitPost()
        {
            if (Post != null)
            {
                Post.Elements = _elements.Select(e => e.Element).ToList();
                base.CommitPost();
            }
        }
    }
}

