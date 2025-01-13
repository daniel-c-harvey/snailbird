using NetBlocks.Models;

namespace SnailbirdMedia.Clients
{
    public interface IVaultManagerClient
    {
        Task<MediaBinary?> GetMedia(string entryKey);
        string MediaURL(string entryKey);
        Task UploadImage();
    }
}