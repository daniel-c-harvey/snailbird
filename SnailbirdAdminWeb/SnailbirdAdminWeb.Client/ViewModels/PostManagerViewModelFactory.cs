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
                                            Navigator<PostManagerMode, 
                                            PostManagerModel<TPost>> navigator);
    
    protected abstract TViewModel CreateViewModel(PostManagerModel<TPost> model,
                                                  Navigator<PostManagerMode, PostManagerModel<TPost>> navigator,
                                                  TUpdate update);
}

// public class PostManagerViewModelFactory<TPost, TEdit> 
// : PostManagerViewModelFactory<TPost, 
//                               TEdit, 
//                               PostManagerUpdate<TPost>, 
//                               PostManagerViewModel<TPost, TEdit, PostManagerUpdate<TPost>>>
// where TPost : Post<TPost>, new()
// where TEdit : EditPostViewModelBase<TPost, TEdit>
// {
//     protected override PostManagerUpdate<TPost> CreateUpdate(IPostManagerClient<TPost> client, Navigator<PostManagerMode, PostManagerModel<TPost>> navigator)
//     {
//         return new PostManagerUpdate<TPost>(client, navigator);
//     }
//
//     protected override PostManagerViewModel<TPost, TEdit, PostManagerUpdate<TPost>> CreateViewModel(PostManagerModel<TPost> model, Navigator<PostManagerMode, PostManagerModel<TPost>> navigator, PostManagerUpdate<TPost> update)
//     {
//         return new PostManagerViewModel<TPost, TEdit, PostManagerUpdate<TPost>>(model, navigator, update);
//     }
// }