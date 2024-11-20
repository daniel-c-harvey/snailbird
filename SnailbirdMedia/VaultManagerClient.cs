using SnailbirdMedia.Configs;
using SnailbirdMedia.Models;
using Newtonsoft.Json;

namespace SnailbirdMedia
{
    public class VaultManagerClient : SnailbirdMediaClient
    {
        public VaultManagerClient(ClientConfig config) 
        : base(config) { }

        public async Task<SnailbirdImage> UploadImage()
        {
            //string json = await (await http.GetAsync("img")).Content.ReadAsStringAsync();
            //MediaBinaryDto? imagePackage = JsonConvert.DeserializeObject<MediaBinaryDto>(json);

            //if (imagePackage != null && imagePackage.Size > 0 && imagePackage.Bytes != null)
            //{
            //    SnailbirdImage image = new(imagePackage.extension, imagePackage.size);
            //    MemoryStream s = new();
            //    var g = Convert.FromBase64String(imagePackage.imageCode);
            //    await s.WriteAsync(g);
            //    s.Position = 0;
            //    await image.LoadStreamAsync(s);

            //    return image;
            //}

            return new SnailbirdImage(string.Empty, 0);
        }
    }
}
