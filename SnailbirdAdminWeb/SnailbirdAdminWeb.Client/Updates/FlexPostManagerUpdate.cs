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

    // Class for allowing additional client dependencies in the Update
    public FlexPostManagerUpdate(IPostManagerClient<TPost> postManager, INavigator<PostManagerMode> navigator) 
    : base(postManager, navigator)
    { }
}