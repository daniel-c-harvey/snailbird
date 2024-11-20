using SnailbirdMedia.Configs;
using SnailbirdMedia.Models;
using System.Text;

namespace SnailbirdMedia
{
    public class SnailbirdMediaClient
    {
        protected HttpClient http { get; set; }

        public SnailbirdMediaClient(ClientConfig config)
        {
            http = new HttpClient();
            try
            {
                http.BaseAddress = new Uri($"{config.BaseURL}:{config.Port}");
            } catch (Exception e) {
                throw;
            }
        }
    }
}
