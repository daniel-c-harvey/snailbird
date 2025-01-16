namespace SnailbirdAdminWeb.Models
{
    public interface IApiEndpoint
    {
        string Name { get; }
        string ApiUrl { get; }
        string ApiKey { get; }
    }

    public class ApiEndpoint : IApiEndpoint
    {
        public string Name { get; set; } = default!;
        public string ApiUrl { get; set; } = default!;
        public string ApiKey { get; set; } = default!;
    }

    public interface IApiEndpoints
    {
        IEnumerable<ApiEndpoint> Endpoints { get; }
    }
    
    public class ApiEndpoints : IApiEndpoints
    {
        public IEnumerable<ApiEndpoint> Endpoints { get; set; } = [];
    }
}
