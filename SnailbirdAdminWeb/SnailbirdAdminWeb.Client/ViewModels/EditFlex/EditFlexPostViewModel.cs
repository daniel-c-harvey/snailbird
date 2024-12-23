using NetBlocks.Utilities;
using SnailbirdAdminWeb.Client.ViewModels.EditFlex.Element;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels.EditFlex
{
    public class EditFlexPostViewModel<TPost> : EditPostViewModelBase<TPost, EditFlexPostViewModel<TPost>>
    where TPost : FlexPost<TPost>, new()
    {
        private List<EditFlexElementViewModel> _elements = new();
        public IList<EditFlexElementViewModel> Elements => _elements;

        public event EventHandler? ElementChanged;

        public EditFlexPostViewModel(Action<TPost> onCommitPost) : base(onCommitPost) { }

        public void AddNewElement(EditFlexElementViewModel element)
        {
            _elements.Add(element);
            ElementsModified();
        }

        private void ElementsModified()
        {
            if (Post != null)
            {
                Post.Elements = _elements.Select(e => e.Element).ToList();
            }
        }

        public void RemoveElement(EditFlexElementViewModel element)
        {
            _elements.Remove(element);
            ElementsModified();
        }

        public override EditFlexPostViewModel<TPost> LoadPost(TPost post)
        {
            base.LoadPost(post);

            if (Post != null)
            {
                _elements = Post.Elements.Select(e => new EditFlexElementViewModel(e)).ToList();

                // Register reordering & delete events
                Elements.Apply(vm =>
                {
                    vm.Ascend += OnElementAscend;
                    vm.Descend += OnElementDescend;
                    vm.DeleteClicked += OnDeleteClicked;
                });
            }

            return this;
        }

        private void OnDeleteClicked(object? sender, EventArgs e)
        {
            if (sender is EditFlexElementViewModel deletedElement)
            {
                RemoveElement(deletedElement);
                ElementChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnElementAscend(object? sender, EventArgs e)
        {
            // re-order the elements.  can probably do a swap with the adjacent elements
            if (sender is EditFlexElementViewModel movedElement)
            {
                int newIndex = Elements.IndexOf(movedElement) - 1;
                if (newIndex >= 0)
                {
                    OnElementReorder(movedElement, newIndex);
                }
            }
        }

        private void OnElementDescend(object? sender, EventArgs e)
        {
            if (sender is EditFlexElementViewModel movedElement)
            {
                int newIndex = Elements.IndexOf(movedElement) + 1;
                if (newIndex < Elements.Count)
                {
                    OnElementReorder(movedElement, newIndex);
                }
            }
        }

        private void OnElementReorder(EditFlexElementViewModel movedElement, int newIndex)
        {
            Elements.Remove(movedElement);
            Elements.Insert(newIndex, movedElement);
            ElementChanged?.Invoke(this, new EventArgs());
        }
    }
}

