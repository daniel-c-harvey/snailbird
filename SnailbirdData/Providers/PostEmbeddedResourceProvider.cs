using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Newtonsoft.Json;
using SnailbirdData.Models;

namespace SnailbirdData.Providers
{
    public class PostEmbeddedResourceProvider
        : IPostProvider
        
    {
        public PostEmbeddedResourceProvider() { }

        TPost IPostProvider.GetPost<TPost>(int id)
        {
            string json = Core.FileLoader.LoadResourceFileAsString(Assembly.GetExecutingAssembly(), 
                                                                   $"SnailbirdData.Data.Posts.post{id}.json");
            
            TPost? obj = JsonConvert.DeserializeObject<TPost>(json);
            return obj ?? throw new Exception();
        }
    }
}
