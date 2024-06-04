using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class LiveJamPostManagerViewModel : PostManagerViewModel<LiveJamPost>
    {
        public LiveJamPostManagerViewModel(IDataAdapter<LiveJamPost> postAdapter)
        : base(postAdapter)
        { }
    }
}
