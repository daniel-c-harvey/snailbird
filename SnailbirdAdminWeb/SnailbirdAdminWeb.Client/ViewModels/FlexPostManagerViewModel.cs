using RazorCore.Navigation;
using RazorCore.Table;
using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdAdminWeb.Client.Updates;
using SnailbirdAdminWeb.Client.ViewModels.EditFlex;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public class FlexPostManagerViewModel<TPost> 
    : PostManagerViewModel<TPost, EditFlexPostViewModel<TPost>, FlexPostManagerUpdate<TPost>>
    where TPost : FlexPost<TPost>, new()
    {
        public FlexPostManagerViewModel(PostManagerModel<TPost> model,
                                        Navigator<PostManagerMode, PostManagerModel<TPost>> navigator,
                                        FlexPostManagerUpdate<TPost> update) 
        : base(model, navigator, update) 
        { }
    }
}
