using Core;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexPostViewModel<TPost> : EditPostViewModelBase<TPost, EditFlexPostViewModel<TPost>>
    where TPost : FlexPost, new()
    {
        private List<EditFlexElementViewModel> _elements = new();
        public IList<EditFlexElementViewModel> Elements => _elements;
        
        public event EventHandler? ElementOrderChanged;

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
            _elements = post.Elements.Select(e => new EditFlexElementViewModel(e))
                                     .OrderBy(vm => vm.Ordinal)
                                     .ToList();
            
            // Register reordering events
            Elements.Apply(vm => vm.OrdinalChanged += OnElementOrdinalChanged);

            return base.LoadPost(post);
        }

        private void OnElementOrdinalChanged(object? sender, EventArgs e)
        {
            // re-order the elements.  can probably do a swap with the adjacent elements
            if (sender is EditFlexElementViewModel movedElement) 
            {
                var replacedElement = Elements.FirstOrDefault(vm => vm.Ordinal == movedElement.Ordinal && !Object.ReferenceEquals(vm, movedElement));
                if (replacedElement != null)
                {
                    Elements.Remove(movedElement);
                    Elements.Insert(movedElement.Ordinal - 1, movedElement);
                    replacedElement.Ordinal = Elements.IndexOf(replacedElement) + 1;
                    ElementOrderChanged?.Invoke(this, new EventArgs());
                }
            }
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

