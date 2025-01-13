using NetBlocks.Models;
using SnailbirdData.Models.Entities;

namespace SnailbirdAdminWeb.Client.API
{
    public class LabPostManagerClient : PostManagerClient<LabFeedFlexPost>
    {
        private static readonly string CONTROLLER_NAME = "labpostmanager";
        public LabPostManagerClient(ClientConfig config) : base(config) { }

        public override async Task<ResultContainer<IEnumerable<LabFeedFlexPost>>> GetPage(int page, int size)
        {
            return await GetPage(page, size, CONTROLLER_NAME);
        }

        public override async Task<Result> Update(LabFeedFlexPost post)
        {
            return await Update(post, CONTROLLER_NAME);
        }

        public override async Task<Result> Insert(LabFeedFlexPost post)
        {
            return await Insert(post, CONTROLLER_NAME);
        }

        public override async Task<Result> Delete(LabFeedFlexPost post)
        {
            return await Delete(post, CONTROLLER_NAME);
        }
    }
}
