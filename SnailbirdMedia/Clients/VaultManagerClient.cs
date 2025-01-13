using NetBlocks.Models;
using SnailbirdMedia.Configs;
using Newtonsoft.Json;

namespace SnailbirdMedia.Clients
{
    public class VaultManagerClient : ApiClient<VaultClientConfig>, IVaultManagerClient
    {

        public VaultManagerClient(VaultClientConfig config)
        : base(config) { }

        public async Task UploadImage()
        {

        }

        public async Task<MediaBinary?> GetMedia(string entryKey)
        {
            string json = await (await http.GetAsync("img")).Content.ReadAsStringAsync();
            MediaBinaryDto? imagePackage = JsonConvert.DeserializeObject<MediaBinaryDto>(json);

            if (imagePackage != null && imagePackage.Size > 0 && imagePackage.Bytes != null)
            {
                return new MediaBinary(imagePackage);
            }

            return null;
        }

        public string MediaURL(string entryKey)
        {
            // guards? GUARDS!!
            return new Uri($"{config.URL}/{config.VaultKey}/{entryKey}").ToString();
        }
    }
}
