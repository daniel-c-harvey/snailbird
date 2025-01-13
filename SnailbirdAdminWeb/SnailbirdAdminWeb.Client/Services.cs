﻿using NetBlocks.Models;
using RazorCore.CanvasImage;
using SnailbirdAdminWeb.Client.API;
using SnailbirdData.Models.Entities;
using SnailbirdMedia.Clients;
using SnailbirdMedia.Configs;

namespace SnailbirdAdminWeb.Client
{
    public static class Services
    {
        public static bool AddClientServices(string baseAddress, IServiceCollection services)
        {
            ClientConfig baseConfig = new(baseAddress);
            StudioPostManagerClient studioManager = new(baseConfig);
            LabPostManagerClient labManager = new(baseConfig);
            ConnectionManagerClient connManager = new(baseConfig);
            VaultManagerClient imageVaultClient = new(new VaultClientConfig(baseAddress, "ABC123", "img")); // todo replace this with a server API call rather than direct media client usage

            services
                .AddSingleton<IPostManagerClient<StudioFeedFlexPost>, StudioPostManagerClient>(_ => studioManager)
                .AddSingleton<IPostManagerClient<LabFeedFlexPost>, LabPostManagerClient>(_ => labManager)
                .AddSingleton<IConnectionManagerClient, ConnectionManagerClient>(_ => connManager)
                .AddSingleton<IVaultManagerClient, VaultManagerClient>(_ => imageVaultClient)
                .AddScoped<ICanvasImageService, CanvasImageService>();

            return true;
        }
    }
}
