using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class FlexPostManagerViewModel : PostManagerViewModel<FlexPost>
    {
        public FlexPostManagerViewModel(IDataAdapter<FlexPost> postAdapter) 
        : base(postAdapter) 
        { }
    }
}
