using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdMedia.Configs
{
    public class ClientConfig
    {
        public string BaseURL { get; }
        public int Port { get; }

        public ClientConfig(string baseURL, int port)
        {
            BaseURL = baseURL;
            Port = port;
        }
    }
}
