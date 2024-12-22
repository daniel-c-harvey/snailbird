﻿using NetBlocks.Models;
using SnailbirdData.Models.Entities;

namespace SnailbirdAdminWeb.Client.API
{
    public class LabPostManagerClient : PostManagerClient<LabFeedFlexPost>
    {
        private static string CONTROLLER_NAME = "labpostmanager";
        public LabPostManagerClient(ClientConfig config) : base(config) { }

        public async override Task<ResultContainer<IEnumerable<LabFeedFlexPost>>> GetPage(int page, int size)
        {
            return await GetPage(page, size, CONTROLLER_NAME);
        }

        public override async Task<Result> Update(LabFeedFlexPost post)
        {
            return await Update(post, CONTROLLER_NAME);
        }
    }
}
