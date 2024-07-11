using Core.Converters;
using RazorCore;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexPostViewModel : EditPostViewModelBase<FlexPost, EditFlexPostViewModel>
    {
        private List<PostElement> _elements = new();
        //public IEnumerable<Instrument> Instruments => _instruments;

        //public static IColumnMap<PostElement> Columns = new ColumnMap<PostElement>()
        //    .AddColumn("",
        //        new ModelColumn<PostElement>(
        //            post => IntConverter.ToString(post.Ordinal),
        //            (post, value) => post.
        //    );

        public EditFlexPostViewModel(Action<FlexPost> onCommitPost) : base(onCommitPost) { }

        //protected virtual IModelColumn<PostElement> PostElementMorpher(PostElement postElement)
        //{

        //}

        public void AddNewElement(PostElement element)
        {
            _elements.Add(element);
        }


        public void RemoveElement(PostElement element)
        {
            _elements.Remove(element);
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

