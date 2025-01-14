using NetBlocks.Models;

namespace SnailbirdMedia.Clients
{
    public interface IVaultManagerClient
    {
        string MediaURL(string entryKey);
        Task<MediaBinary?> GetMedia(string entryKey);
        Task PostImage(string entryKey, MediaBinary model);
    }
}