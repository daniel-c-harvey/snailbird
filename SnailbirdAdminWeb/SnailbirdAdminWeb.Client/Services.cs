using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SnailbirdAdminWeb.Client.API;
using SnailbirdData.Models.Entities;
using SnailbirdMedia.Clients;
using SnailbirdMedia.Configs;

namespace SnailbirdAdminWeb.Client
{
    public class Services
    {
        public static bool AddClientServices(string baseAddress, IServiceCollection services)
        {
            StudioPostManagerClient studioManager = new(new NetBlocks.Models.ClientConfig(baseAddress));
            LabPostManagerClient labManager = new(new NetBlocks.Models.ClientConfig(baseAddress));
            VaultManagerClient imageVaultClient = new(new VaultClientConfig(baseAddress, "ABC123", "img")); // todo replace this with a server API call rather than direct media client usage

            services
                .AddSingleton<IPostManagerClient<StudioFeedFlexPost>, StudioPostManagerClient>(_ => studioManager)
                .AddSingleton<IPostManagerClient<LabFeedFlexPost>, LabPostManagerClient>(_ => labManager)
                .AddSingleton<IVaultManagerClient, VaultManagerClient>(_ => imageVaultClient);

            return true;
        }
    }
}
