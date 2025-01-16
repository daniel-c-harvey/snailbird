using DataAccess;
using NetBlocks.Models;
using SnailbirdData.Models.Post;
using SnailbirdMedia.Clients;

namespace SnailbirdAdminWeb.API.Managers;

public class FlexPostManager<TPost> : PostManager<TPost>
    where TPost : FlexPost<TPost>, new()
{
    private IVaultManagerClient VaultManagerClient { get; }

    public FlexPostManager(IDataAdapter<TPost> postAdapter, IVaultManagerClient vaultManagerClient)
        : base(postAdapter)
    {
        VaultManagerClient = vaultManagerClient;
    }

    public override async Task<ResultContainer<IEnumerable<TPost>>> GetPosts(int pageIndex, int pageSize)
    {
        var getResults = await base.GetPosts(pageIndex, pageSize);
        if (!getResults.Success) return getResults;

        foreach (TPost post in getResults.Value ?? [])
        {
            foreach (FlexElement element in post.Elements)
            {
                if (element is FlexImage image && !string.IsNullOrEmpty(image.ImageUri))
                {
                    var media = await VaultManagerClient.GetMedia(image.ImageUri);
                    if (media is null)
                    {
                        getResults.Fail($"Failed to load image media for {image.ImageUri}");
                    }
                    else
                    {
                        image.Image = media;
                    }
                }
            }
        }
        return getResults;
    }

    public override async Task<Result> SavePost(TPost post)
    {
        Stack<Task> saveTasks = new();
        
        // Dispatch save tasks
        foreach (FlexElement element in post.Elements)
        {
            if (element is FlexImage image && image.Image != null && !string.IsNullOrEmpty(image.ImageUri))
            {
                saveTasks.Push(VaultManagerClient.PostMedia(image.ImageUri, image.Image));
            }
        }
        Task<Result> saveTask = base.SavePost(post);

        // await all saves
        while (saveTasks.Pop() is Task save) await save;
        return await saveTask;
    }
}