using Microsoft.AspNetCore.Components;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models;

namespace SnailbirdAdmin.Components.Pages
{
    public partial class NewPost
    {

        [Inject]
        IDataAdapter<LiveJamPost> PostAdapter { get; set; } = default!;

        private LiveJamPost Post { get; } = new LiveJamPost();

        private List<LiveJamPostInstrument> _instruments = new List<LiveJamPostInstrument>();
        private IEnumerable<LiveJamPostInstrument> Instruments => _instruments;

        private void AddNewInstrument()
        {
            _instruments.Add(new LiveJamPostInstrument());
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
