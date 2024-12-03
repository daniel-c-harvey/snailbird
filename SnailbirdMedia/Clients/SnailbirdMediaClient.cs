using SnailbirdMedia.Configs;

namespace SnailbirdMedia.Clients
{
    public abstract class SnailbirdMediaClient<TConfig>
        where TConfig : ClientConfig
    {
        protected TConfig config;
        protected HttpClient http { get; set; }

        public SnailbirdMediaClient(TConfig config)
        {
            this.config = config;
            http = new HttpClient();
            try
            {
                http.BaseAddress = new Uri(config.URL);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
