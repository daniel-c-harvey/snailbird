using SnailbirdMedia.Configs;
using SnailbirdMedia.Models;
using Newtonsoft.Json;
using NetBlocks.Models;

namespace SnailbirdMedia.Clients
{
    public class VaultManagerClient : SnailbirdMediaClient<VaultClientConfig>
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
