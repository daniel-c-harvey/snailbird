using System.Net.Http.Json;
using NetBlocks.Models;
using SnailbirdMedia.Configs;

namespace SnailbirdMedia.Clients
{
    public interface IVaultManagerClient
    {
        public string VaultKey { get; }
        Task<MediaBinary?> GetMedia(string entryKey);
        Task PostMedia(string entryKey, MediaBinary model);
    }
    
    public abstract class VaultManagerClient : ApiClient<VaultClientConfig>, IVaultManagerClient
    {
        public VaultManagerClient(VaultClientConfig config)
        : base(config) { }

        public abstract string VaultKey { get; } 
        
        public async Task<MediaBinary?> GetMedia(string entryKey)
        {
            try
            {
                MediaBinaryDto? imagePackage = await http.GetFromJsonAsync<MediaBinaryDto>(MediaUrl(entryKey));

                return imagePackage is { Size: > 0 } ? new MediaBinary(imagePackage) : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task PostMedia(string entryKey, MediaBinary model)
        {
            try
            {
                await http.PostAsJsonAsync(MediaUrl(entryKey), MediaBinaryDto.From(model));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private string MediaUrl(string entryKey)
        {
            // guards? GUARDS!!
            return new Uri($"{config.VaultKey}/{entryKey}").ToString();
        }
    }
}
