namespace SnailbirdAdminWeb.Models
{
    public interface IEndpoints
    {
        string MediaApiUrl { get; }
        string MediaApiKey { get; }
    }

    public class Endpoints : IEndpoints
    {
        public string MediaApiUrl { get; set; } = default!;
        public string MediaApiKey { get; set; } = default!;
    }
}
