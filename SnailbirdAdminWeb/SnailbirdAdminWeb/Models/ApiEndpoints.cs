namespace SnailbirdAdminWeb.Models
{
    public interface IEndpoint
    {
        string MediaApiUrl { get; }
        string MediaApiKey { get; }
    }

    public class Endpoint : IEndpoint
    {
        public string MediaApiUrl { get; set; } = default!;
        public string MediaApiKey { get; set; } = default!;
    }

    public interface IApiEndpoints
    {
        IEnumerable<Endpoint> Endpoints { get; set; }
    }
    
    public class ApiEndpoints : IApiEndpoints
    {
        public IEnumerable<Endpoint> Endpoints { get; set; } = [];
    }
}
