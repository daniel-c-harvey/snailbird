using Microsoft.AspNetCore.Components;
using RazorCore;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models;

namespace SnailbirdAdmin.Components.Elements
{
    public partial class EditPost
    {
        [Parameter]
        public Action<LiveJamPost> OnCommitPost { get; set; } = default!;
        
        [Parameter]
        public LiveJamPost Post { get; set; }

        private List<LiveJamPostInstrument> _instruments = new List<LiveJamPostInstrument>();
        private IEnumerable<LiveJamPostInstrument> Instruments => _instruments;

        private static IColumnMap<LiveJamPostInstrument> InstrumentColumns = new ColumnMap<LiveJamPostInstrument>()
            .AddColumn("Name",
                new ModelColumn<LiveJamPostInstrument>(
                    inst => inst.Name,
                    (inst, name) => inst.Name = name)
                .MakeEditable())
            .AddColumn("Description",
                new ModelColumn<LiveJamPostInstrument>(
                    inst => inst.Description,
                    (inst, desc) => inst.Description = desc)
                .MakeEditable());

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _instruments = Post.Instruments.ToList();
        }

        private void AddNewInstrument(LiveJamPostInstrument instrument)
        {
            _instruments.Add(instrument);
        }


        private void RemoveInstrument(LiveJamPostInstrument instrument)
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