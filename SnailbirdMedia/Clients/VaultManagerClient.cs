using System.Net.Http.Json;
using NetBlocks.Models;
using SnailbirdMedia.Configs;

namespace SnailbirdMedia.Clients
{
    public interface IVaultManagerClient
    {
        public string VaultKey { get; }
        Task<ResultContainer<MediaBinary>> GetMedia(string entryKey);
        Task<Result> PostMedia(string entryKey, MediaBinary model);
    }
    
    public abstract class VaultManagerClient : ApiClient<VaultClientConfig>, IVaultManagerClient
    {
        public VaultManagerClient(VaultClientConfig config) : base(config) { }
        
        public abstract string VaultKey { get; } 
        
        public async Task<ResultContainer<MediaBinary>> GetMedia(string entryKey)
        {
            try
            {
                MediaBinaryDto? imagePackage = await http.GetFromJsonAsync<MediaBinaryDto>(MediaUrl(entryKey));
                if (imagePackage == null) return ResultContainer<MediaBinary>.CreateFailResult($"No resource found for key {entryKey}");
                
                return new ResultContainer<MediaBinary>
                {
                    Value = new MediaBinary(imagePackage)
                };                
            }
            catch (Exception e)
            {
                return ResultContainer<MediaBinary>.CreateFailResult(e.Message);
            }
        }

        public async Task<Result> PostMedia(string entryKey, MediaBinary model)
        {
            try
            {
                var response = await http.PostAsJsonAsync(MediaUrl(entryKey), MediaBinaryDto.From(model));

                if (!response.IsSuccessStatusCode)
                {
                    return Result.CreateFailResult($"Failed to create media for key {entryKey}")
                        .Fail(await response.Content.ReadAsStringAsync());
                }

                return Result.CreatePassResult();
            }
            catch (Exception e)
            {
                return Result.CreateFailResult(e.Message);
            }
        }

        private string MediaUrl(string entryKey)
        {
            // guards? GUARDS!!
            return new Uri($"{config.VaultKey}/{entryKey}").ToString();
        }
    }
}
