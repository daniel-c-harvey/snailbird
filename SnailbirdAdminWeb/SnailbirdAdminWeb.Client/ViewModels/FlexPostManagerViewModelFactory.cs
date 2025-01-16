using NetBlocks.Models;
using RazorCore.Navigation;
using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdAdminWeb.Client.Updates;
using SnailbirdAdminWeb.Client.ViewModels.EditFlex;

using SnailbirdData.Models.Post;
using SnailbirdMedia.Clients;

namespace SnailbirdAdminWeb.Client.ViewModels;

public class FlexPostManagerViewModelFactory<TPost> 
: PostManagerViewModelFactory<TPost, 
                              EditFlexPostViewModel<TPost>, 
                              FlexPostManagerUpdate<TPost>, 
                              FlexPostManagerViewModel<TPost>>
where TPost : FlexPost<TPost>, new()
{
    protected override FlexPostManagerUpdate<TPost> CreateUpdate(IPostManagerClient<TPost> client, 
                                                                 Navigator<PostManagerMode, PostManagerModel<TPost>> navigator)
    {
        return new FlexPostManagerUpdate<TPost>(client, navigator);
    }

    protected override FlexPostManagerViewModel<TPost> CreateViewModel(PostManagerModel<TPost> model,
                                                                       Navigator<PostManagerMode, PostManagerModel<TPost>> navigator,
                                                                       FlexPostManagerUpdate<TPost> update)
    {
        return new FlexPostManagerViewModel<TPost>(model, navigator, update);
    }
}