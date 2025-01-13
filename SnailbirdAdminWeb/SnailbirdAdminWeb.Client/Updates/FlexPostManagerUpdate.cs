using RazorCore.Navigation;
using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.Messages;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdData.Models.Post;
using SnailbirdMedia.Clients;

namespace SnailbirdAdminWeb.Client.Updates;

public class FlexPostManagerUpdate<TPost> : PostManagerUpdate<TPost>
where TPost : FlexPost<TPost>, new()
{
    private readonly IVaultManagerClient _mediaClient;

    // TODO inject the media client to use during override of the save new and save functions
    public FlexPostManagerUpdate(IPostManagerClient<TPost> postManager, INavigator<PostManagerMode> navigator, IVaultManagerClient mediaClient) 
    : base(postManager, navigator)
    {
        _mediaClient = mediaClient;
    }

    protected override void SavePost(PostManagerModel<TPost> model, PostManagerSaveExistingMessage<TPost> message)
    {
        SavePostMedia(model.Post);
        base.SavePost(model, message);
    }

    private void SavePostMedia(TPost modelPost)
    {
        foreach (FlexElement element in modelPost.Elements)
        {
            if (element is FlexImage { Image: not null } image)
            {
                _mediaClient.PostImage(image.ImageUri, image.Image);
            }
        }
    }
}