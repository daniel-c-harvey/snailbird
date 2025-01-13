using NetBlocks.Models;

namespace SnailbirdMedia.Clients
{
    public interface IVaultManagerClient
    {
        Task<MediaBinary?> GetMedia(string entryKey);
        Task PostImage(string entryKey, MediaBinary model);
    }
}