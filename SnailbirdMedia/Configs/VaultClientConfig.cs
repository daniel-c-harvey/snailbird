using NetBlocks.Models;

namespace SnailbirdMedia.Configs
{
    public class VaultClientConfig : ClientConfig
    {
        public string VaultKey { get; }
        public string ApiKey { get; }

        public VaultClientConfig(string url, string apiKey, string vaultKey) : base(url)
        {
            VaultKey = vaultKey;
            ApiKey = apiKey;
        }

        public VaultClientConfig(string baseURL, int port, string apiKey, string vaultKey) : base(baseURL, port)
        {
            VaultKey = vaultKey;
            ApiKey = apiKey;
        }
    }
}
