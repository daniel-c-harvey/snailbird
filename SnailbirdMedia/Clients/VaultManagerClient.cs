using System.Net.Http.Json;
using NetBlocks.Models;
using SnailbirdMedia.Configs;

namespace SnailbirdMedia.Clients
{
    public class VaultManagerClient : ApiClient<VaultClientConfig>, IVaultManagerClient
    {
        public VaultManagerClient(VaultClientConfig config)
        : base(config) { }

        public async Task<MediaBinary?> GetMedia(string entryKey)
        {
            MediaBinaryDto? imagePackage = await http.GetFromJsonAsync<MediaBinaryDto>(MediaUrl(entryKey));

            return imagePackage is { Size: > 0 } ? new MediaBinary(imagePackage) : null;
        }

        public async Task PostImage(string entryKey, MediaBinary model)
        {
            await http.PostAsJsonAsync(MediaUrl(entryKey), MediaBinaryDto.From(model));
        }

        private string MediaUrl(string entryKey)
        {
            // guards? GUARDS!!
            return new Uri($"{config.VaultKey}/{entryKey}").ToString();
        }
    }
}
