using RazorCore.Navigation;
using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.Updates;

public class FlexPostManagerUpdate<TPost> : PostManagerUpdate<TPost>
where TPost : Post<TPost>, new()
{
    // TODO inject the media client to use during override of the save new and save functions
    public FlexPostManagerUpdate(IPostManagerClient<TPost> postManager, INavigator<PostManagerMode> navigator) 
    : base(postManager, navigator)
    {
        
    }
}