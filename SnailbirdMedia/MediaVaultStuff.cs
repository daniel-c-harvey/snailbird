using NetBlocks.Models.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdMedia
{
    public class MediaVaultStuff // todo rename me!
    {
        public string BaseURL { get; set; }
        public string Vault {  get; set; }
        public MediaVaultStuff(string baseUrl, string vault)
        {
            BaseURL = baseUrl;
            Vault = vault;
        }

        public string MediaURL(string entryKey)
        {
            // guards? GUARDS!!
            return new($"{BaseURL}/{Vault}/{entryKey}");
        }
    }
}
