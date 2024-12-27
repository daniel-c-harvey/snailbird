using NetBlocks.Models;
using SnailbirdData.Models.Entities;

namespace SnailbirdAdminWeb.Client.API
{
    public class StudioPostManagerClient : PostManagerClient<StudioFeedFlexPost>
    {
        private static string CONTROLLER_NAME = "studiopostmanager";
        public StudioPostManagerClient(ClientConfig config) : base(config) { }

        public async override Task<ResultContainer<IEnumerable<StudioFeedFlexPost>>> GetPage(int page, int size)
        {
            return await GetPage(page, size, CONTROLLER_NAME);
        }

        public override async Task<Result> Update(StudioFeedFlexPost post)
        {
            return await Update(post, CONTROLLER_NAME);
        }

        public async override Task<Result> Insert(StudioFeedFlexPost post)
        {
            return await Insert(post, CONTROLLER_NAME);
        }

        public async override Task<Result> Delete(StudioFeedFlexPost post)
        {
            return await Delete(post, CONTROLLER_NAME);
        }
    }
}
