using NetBlocks.Models.FileBinary;
using SnailbirdMedia.Configs;

namespace SnailbirdMedia.Clients;

public interface IImageVaultManagerClient : IVaultManagerClient<ImageBinary, ImageBinaryDto, ImageBinaryParams> { }

public class ImageVaultManagerClient : VaultManagerClient<ImageBinary, ImageBinaryDto, ImageBinaryParams>, IImageVaultManagerClient
{
    public ImageVaultManagerClient(VaultClientConfig config) : base(config) { }
    
    public override string VaultKey => "img";
}