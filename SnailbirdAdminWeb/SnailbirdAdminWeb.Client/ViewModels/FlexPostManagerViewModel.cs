using DataAccess;
using SnailbirdAdminWeb.Client.API;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public class FlexPostManagerViewModel<TPost> : PostManagerViewModel<TPost>
    where TPost : FlexPost<TPost>, new()
    {
        public FlexPostManagerViewModel(IPostManagerClient<TPost> postManager) 
        : base(postManager) 
        { }
    }
}
