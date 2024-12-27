using NetBlocks.Models;
using NetBlocks.Models.Environment;
using SnailbirdData.Models.Post;
using System.Net.Http.Json;

namespace SnailbirdAdminWeb.Client.API
{
    public class ConnectionManagerClient : ApiClient<ClientConfig>, IConnectionManagerClient
    {
        private const string CONTROLLER = "connections";
        public ConnectionManagerClient(ClientConfig config) : base(config) { }

        public async Task<ResultContainer<Connections>> GetConnections()
        {
            var connResults = await http.GetFromJsonAsync<ResultContainer<Connections>>($"api/{CONTROLLER}");
            if (connResults is null || !connResults.Success || connResults.Value is null)
            {
                return ResultContainer<Connections>.CreateFailResult("Failed to load connections");
            }
            return new ResultContainer<Connections>(connResults.Value);
        }

        public async Task<Result> SaveConnections(Connections connections)
        {
            Result? result = null;
            try
            {
                HttpResponseMessage response = await http.PostAsJsonAsync($"api/{CONTROLLER}", connections);
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
