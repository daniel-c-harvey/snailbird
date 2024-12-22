using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SnailbirdAdminWeb.Client.API;
using SnailbirdData.Models.Entities;
using SnailbirdMedia.Clients;
using SnailbirdMedia.Configs;

namespace SnailbirdAdminWeb.Client
{
    public class Services
    {
        public static bool AddClientServices(string address, IServiceCollection services)
        {
            LabPostManagerClient manager = new(new NetBlocks.Models.ClientConfig(address));
            VaultManagerClient imageVaultClient = new(new VaultClientConfig(address, "ABC123", "img")); // todo replace this with a server API call rather than direct media client usage

            services
                .AddSingleton<IPostManagerClient<LabFeedFlexPost>, LabPostManagerClient>(_ => manager)
                .AddSingleton<IVaultManagerClient, VaultManagerClient>(_ => imageVaultClient);

            return true;
        }
    }
}
