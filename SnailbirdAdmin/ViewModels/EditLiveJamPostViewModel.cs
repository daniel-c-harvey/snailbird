using RazorCore;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditLiveJamPostViewModel : EditPostViewModelBase<LiveJamPost, EditLiveJamPostViewModel>
    {
        private List<Instrument> _instruments = new List<Instrument>();
        public IEnumerable<Instrument> Instruments => _instruments;

        public static IColumnMap<Instrument> InstrumentColumns = new ColumnMap<Instrument>()
            .AddColumn("Name",
                new ModelColumn<Instrument>(
                    inst => inst.Name,
                    (inst, name) => inst.Name = name)
                .MakeEditable())
            .AddColumn("Description",
                new ModelColumn<Instrument>(
                    inst => inst.Description,
                    (inst, desc) => inst.Description = desc)
                .MakeEditable());

        public EditLiveJamPostViewModel(Action<LiveJamPost> onCommitPost) 
        : base(onCommitPost) 
        { }

        public void AddNewInstrument(Instrument instrument)
        {
            _instruments.Add(instrument);
        }


        public void RemoveInstrument(Instrument instrument)
        {
            _instruments.Remove(instrument);
        }

        public override EditLiveJamPostViewModel LoadPost(LiveJamPost post)
        {
            _instruments = post.Instruments.ToList();
            return base.LoadPost(post);
        }

        public override void CommitPost()
        {
            if (Post != null)
            {
                Post.Instruments = _instruments;
                base.CommitPost();
            }
        }
    }
}

