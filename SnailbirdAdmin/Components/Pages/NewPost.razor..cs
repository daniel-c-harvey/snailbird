using Microsoft.AspNetCore.Components;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models;

namespace SnailbirdAdmin.Components.Pages
{
    public partial class NewPost
    {
        private class InstrumentRow
        {
            public bool IsNew { get; }
            public LiveJamPostInstrument Instrument { get; }
            public InstrumentRow(LiveJamPostInstrument instrument, bool isNew)
            {
                Instrument = instrument;
                IsNew = isNew;
            }
        }

        [Inject]
        IDataAdapter<LiveJamPost> PostAdapter { get; set; } = default!;

        private LiveJamPost Post { get; } = new LiveJamPost();

        private LiveJamPostInstrument NewInstrument { get; set; } = new LiveJamPostInstrument();

        private List<LiveJamPostInstrument> _instruments = new List<LiveJamPostInstrument>();
        private IEnumerable<InstrumentRow> Instruments => _instruments.Select(i => new InstrumentRow(i, false)).Append(new InstrumentRow(NewInstrument, true));

        private void AddNewInstrument()
        {
            _instruments.Add(NewInstrument);
            NewInstrument = new LiveJamPostInstrument();
        }


        private void RemoveInstrument(LiveJamPostInstrument instrument)
        {
            _instruments.Remove(instrument);
        }

        private void CommitPost()
        {
            Post.Instruments = _instruments;
            PostAdapter.Insert(Post);
        }
    }
}
