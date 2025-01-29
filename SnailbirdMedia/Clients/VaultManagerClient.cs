using System.Net.Http.Json;
using NetBlocks.Models;
using NetBlocks.Models.FileBinary;
using NetBlocks.Models.FileBinary.Factory;
using SnailbirdMedia.Configs;

namespace SnailbirdMedia.Clients
{
    public interface IVaultManagerClient<TMedia, TDto, TParams> 
    where TMedia : MediaBinary<TMedia, TDto, TParams>, new()
    where TDto : MediaBinaryDto<TMedia, TDto, TParams>, new()
    where TParams : MediaBinaryParams
    {
        public string VaultKey { get; }
        Task<ResultContainer<TMedia>> GetMedia(string entryKey);
        Task<Result> PostMedia(string entryKey, TMedia model);
    }
    
    public abstract class VaultManagerClient<TMedia, TDto, TParams> : ApiClient<VaultClientConfig>, IVaultManagerClient<TMedia, TDto, TParams>
        where TMedia : MediaBinary<TMedia, TDto, TParams>, new()
        where TDto : MediaBinaryDto<TMedia, TDto, TParams>, new()
        where TParams : MediaBinaryParams
    {
        protected VaultManagerClient(VaultClientConfig config) : base(config)
        {
            http.DefaultRequestHeaders.Add("ApiKey", config.ApiKey);
        }
        
        public abstract string VaultKey { get; } 
        
        public async Task<ResultContainer<TMedia>> GetMedia(string entryKey)
        {
            try
            {
                string? ip = await http.GetStringAsync(MediaUrl(entryKey));
                TDto? imagePackage = await http.GetFromJsonAsync<TDto>(MediaUrl(entryKey));
                if (imagePackage == null) return ResultContainer<TMedia>.CreateFailResult($"No resource found for key {entryKey}");
                
                return new ResultContainer<TMedia>
                {
                    Value = MediaFactory.CreateFromDto<TMedia, TDto, TParams>(imagePackage)
                };                
            }
            catch (Exception e)
            {
                return ResultContainer<TMedia>.CreateFailResult(e.Message);
            }
        }

        public async Task<Result> PostMedia(string entryKey, TMedia model)
        {
            try
            {
                var response = await http.PostAsJsonAsync(MediaUrl(entryKey), DtoFactory.CreateFromMedia<TMedia, TDto, TParams>(model));

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
            return $"manage/{config.VaultKey}/{entryKey}";
        }
    }
}
