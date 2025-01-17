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
                if (element is not FlexImage image || string.IsNullOrEmpty(image.ImageUri)) continue;
                
                var mediaResults = await VaultManagerClient.GetMedia(image.ImageUri);
                if (!mediaResults.Success || mediaResults.Value is null)
                {
                    getResults.Fail($"Failed to load image media for {image.ImageUri}");
                }
                else
                {
                    image.Image = mediaResults.Value;
                }
            }
        }
        return getResults;
    }

    public override async Task<Result> SavePost(TPost post)
    {
        Result result = new();
        Queue<Task<Result>> saveTasks = new();
        
        // Dispatch save tasks
        foreach (FlexElement element in post.Elements)
        {
            if (element is FlexImage { Image: not null } image && !string.IsNullOrEmpty(image.ImageUri))
            {
                saveTasks.Enqueue(VaultManagerClient.PostMedia(image.ImageUri, image.Image));
            }
        }
        saveTasks.Enqueue(base.SavePost(post));

        // await all saves and gather results
        while (saveTasks.Any())
        {
            Task<Result> save = saveTasks.Dequeue();
            result.Merge((await save));
        }
        
        return result;
    }
}