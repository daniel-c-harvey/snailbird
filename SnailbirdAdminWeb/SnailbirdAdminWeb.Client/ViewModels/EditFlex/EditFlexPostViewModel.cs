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
            Elements.Add(element);
            RegisterElementEvents(element);
            OnElementChanged();
        }

        public void RemoveElement(EditFlexElementViewModel element)
        {
            Elements.Remove(element);
            OnElementChanged();
        }

        public override EditFlexPostViewModel<TPost> LoadPost(TPost post)
        {
            base.LoadPost(post);

            if (Post != null)
            {
                _elements = Post.Elements.Select(e => new EditFlexElementViewModel(e)).ToList();

                // Register reordering & delete events
                Elements.Apply(RegisterElementEvents);
            }

            return this;
        }

        private void RegisterElementEvents(EditFlexElementViewModel model)
        {
            model.Ascend += OnElementAscend;
            model.Descend += OnElementDescend;
            model.DeleteClicked += OnDeleteClicked;
            model.ElementChanged += OnElementChanged;
        }

        private void OnDeleteClicked(object? sender, EventArgs e)
        {
            if (sender is EditFlexElementViewModel deletedElement)
            {
                RemoveElement(deletedElement);
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
            OnElementChanged();
        }

        private void OnElementChanged(object? sender, EventArgs e)
        {
            OnElementChanged();
        }

        private void OnElementChanged()
        {
            ElementsModified();
            ElementChanged?.Invoke(this, new EventArgs());
        }

        private void ElementsModified()
        {
            if (Post != null)
            {
                Post.Elements = Elements.Select(e => e.Element).ToList();
            }
        }
    }
}

