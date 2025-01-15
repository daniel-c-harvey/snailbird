using RazorCore.Navigation;
using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdAdminWeb.Client.Updates;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels;

public abstract class PostManagerViewModelFactory<TPost, TEdit, TUpdate, TViewModel>
where TPost : Post<TPost>, new()
where TEdit : EditPostViewModelBase<TPost, TEdit>
where TUpdate : PostManagerUpdate<TPost>
where TViewModel : PostManagerViewModel<TPost, TEdit, TUpdate>
{
    public TViewModel Create(IPostManagerClient<TPost> client)
    {
        var model = new PostManagerModel<TPost>();
        var navigator = new Navigator<PostManagerMode, PostManagerModel<TPost>>(model);
        TUpdate update = CreateUpdate(client, navigator);
        var vm = CreateViewModel(model, navigator, update);
        vm.InitColumnMap();
        return vm;
    }

    protected abstract TUpdate CreateUpdate(IPostManagerClient<TPost> client,
                                            Navigator<PostManagerMode, PostManagerModel<TPost>> navigator);
    
    protected abstract TViewModel CreateViewModel(PostManagerModel<TPost> model,
                                                  Navigator<PostManagerMode, PostManagerModel<TPost>> navigator,
                                                  TUpdate update);
}