using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdMedia.Configs
{
    public class ClientConfig
    {
        public string URL { get; }

        public ClientConfig(string baseURL, int port)
        {
            URL = $"{baseURL}:{port}";
        }

        public ClientConfig(string url)
        {
            URL = url;
        }

    }
}
