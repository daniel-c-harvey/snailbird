using NetBlocks.Models;
using SnailbirdData.Models.Post;
using System.Net.Http.Json;

namespace SnailbirdAdminWeb.Client.API
{
    public class PostManagerClient<TPost> : ApiClient<ClientConfig>, IPostManagerClient<TPost> where TPost : Post<TPost>
    {
        public PostManagerClient(ClientConfig config) : base(config) { }

        public async Task<ResultContainer<IEnumerable<TPost>>> GetPage(int page, int size)
        {
            ResultContainer<IEnumerable<TPost>>? result = null;
            try
            {
                HttpResponseMessage get = await http.GetAsync($"api/labpostmanager?page=1&size=10");
                result = await get.Content.ReadFromJsonAsync<ResultContainer<IEnumerable<TPost>>>();

                if (result == null)
                {
                    throw new Exception($"Failed to deserialize post page.");
                }
            }
            catch (Exception ex)
            {
                result = new();
                result.Fail(ex.Message);
            }

            return result;
        }
    }
}
