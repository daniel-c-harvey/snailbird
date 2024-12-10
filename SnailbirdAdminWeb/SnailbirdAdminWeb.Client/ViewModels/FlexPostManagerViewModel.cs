using DataAccess;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public class FlexPostManagerViewModel<TPost> : PostManagerViewModel<TPost>
    where TPost : FlexPost<TPost>, new()
    {
        public FlexPostManagerViewModel(IDataAdapter<TPost> postAdapter) 
        : base(postAdapter) 
        { }
    }
}
