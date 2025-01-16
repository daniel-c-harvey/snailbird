using SnailbirdMedia.Configs;

namespace SnailbirdMedia.Clients;

public interface IImageVaultManagerClient : IVaultManagerClient { }

public class ImageVaultManagerClient : VaultManagerClient, IImageVaultManagerClient
{
    public ImageVaultManagerClient(VaultClientConfig config) : base(config) { }
    
    public override string VaultKey => "img";
}