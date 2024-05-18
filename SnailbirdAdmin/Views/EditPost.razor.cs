using Microsoft.AspNetCore.Components;
using RazorCore;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Views
{
    public partial class EditPost
    {
        [Parameter]
        public Action<LiveJamPost> OnCommitPost { get; set; } = default!;

        [Parameter]
        public LiveJamPost Post { get; set; }

        private List<Instrument> _instruments = new List<Instrument>();
        private IEnumerable<Instrument> Instruments => _instruments;

        private static IColumnMap<Instrument> InstrumentColumns = new ColumnMap<Instrument>()
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

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _instruments = Post.Instruments.ToList();
        }

        private void AddNewInstrument(Instrument instrument)
        {
            _instruments.Add(instrument);
        }


        private void RemoveInstrument(Instrument instrument)
        {
            _instruments.Remove(instrument);
        }

        private void CommitPost()
        {
            Post.Instruments = _instruments;
            OnCommitPost(Post);
        }
    }
}