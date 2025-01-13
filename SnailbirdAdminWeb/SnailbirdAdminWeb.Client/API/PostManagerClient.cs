using NetBlocks.Models;
using SnailbirdData.Models.Post;
using System.Net.Http.Json;

namespace SnailbirdAdminWeb.Client.API
{
    public abstract class PostManagerClient<TPost> : ApiClient<ClientConfig>, IPostManagerClient<TPost> where TPost : Post<TPost>
    {
        public PostManagerClient(ClientConfig config) : base(config) { }

        public abstract Task<ResultContainer<IEnumerable<TPost>>> GetPage(int page, int size);
        protected async Task<ResultContainer<IEnumerable<TPost>>> GetPage(int page, int size, string controller)
        {
            ResultContainer<IEnumerable<TPost>>? result = null;
            try
            {
                HttpResponseMessage get = await http.GetAsync($"api/{controller}?page={page}&size={size}");
                result = await get.Content.ReadFromJsonAsync<ResultContainer<IEnumerable<TPost>>>();
                if (result?.Value is null)
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

        public abstract Task<Result> Update(TPost post);
        protected async Task<Result> Update(TPost post, string controller)
        {
            Result? result = null;
            try
            {
                HttpResponseMessage response = await http.PostAsJsonAsync($"api/{controller}/save", post);
                result = await response.Content.ReadFromJsonAsync<Result>();
                if (result is null)
                {
                    throw new Exception("Failed to deserialize the results");
                }
            }
            catch (Exception ex)
            {
                result = new();
                result.Fail(ex.Message);
            }
            return result;
        }


        public abstract Task<Result> Insert(TPost post);
        protected async Task<Result> Insert(TPost post, string controller)
        {
            Result? result = null;
            try
            {
                HttpResponseMessage response = await http.PostAsJsonAsync($"api/{controller}/insert", post);
                result = await response.Content.ReadFromJsonAsync<Result>();
                if (result is null)
                {
                    throw new Exception("Failed to deserialize the results");
                }
            }
            catch (Exception ex)
            {
                result = new();
                result.Fail(ex.Message);
            }
            return result;
        }

        public abstract Task<Result> Delete(TPost post);
        protected async Task<Result> Delete(TPost post, string controller)
        {
            Result? result = null;
            try
            {
                HttpResponseMessage response = await http.PostAsJsonAsync<TPost>($"api/{controller}/delete", post);
                result = await response.Content.ReadFromJsonAsync<Result>();
                if (result is null)
                {
                    throw new Exception("Failed to deserialize the results");
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
