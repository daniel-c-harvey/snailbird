using Microsoft.AspNetCore.Components;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models;

namespace SnailbirdAdmin.Components.Pages
{
    public partial class NewPost
    {

        [Inject]
        IDataAdapter<LiveJamPost> PostAdapter { get; set; } = default!;

        [Parameter]
        public int SuggestedID { get; set; } = 0;

        private LiveJamPost Post { get; } = new LiveJamPost();

        private List<LiveJamPostInstrument> _instruments = new List<LiveJamPostInstrument>();
        private IEnumerable<LiveJamPostInstrument> Instruments => _instruments;

        protected override void OnInitialized()
        {
            Post.ID = SuggestedID;
            Post.PostDate = DateTime.Today;
        }

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
