using SnailbirdMedia.Configs;

namespace SnailbirdMedia.Clients;

public interface IVideoVaultManagerClient : IVaultManagerClient { }

public class VideoVaultManagerClient : VaultManagerClient, IVideoVaultManagerClient
{
    public VideoVaultManagerClient(VaultClientConfig config) : base(config) { }

    public override string VaultKey => "vid";
}