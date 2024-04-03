using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SnailbirdData.Providers
{
    public class PostEmbeddedResourceProvider : IPostProvider        
    {
        public TPost GetPost<TPost>(int id) where TPost : Models.Post
        {
            string json = Core.FileLoader.LoadResourceFileAsString(Assembly.GetExecutingAssembly(), 
                                                                   $"SnailbirdData.Data.Posts.post{id}.json");
            
            TPost? obj = JsonConvert.DeserializeObject<TPost>(json);
            return obj ?? throw new Exception();
        }

        public IEnumerable<TPost> GetRecentPosts<TPost>(int pageIndex, int pageLength) where TPost : Models.Post
        {
            IEnumerable<string> postNames = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where((resName) => resName.StartsWith("SnailbirdData.Data.Posts."));
            var posts = new List<TPost>(postNames.Count());
            
            foreach(string name in postNames)
            {
                Match matchID = Regex.Match(name, @"SnailbirdData[.]Data[.]Posts[.]post(\d+)[.]json");
                if (int.TryParse(matchID.Captures.FirstOrDefault()?.Value, out int postID))
                {
                    posts.Add(GetPost<TPost>(postID));
                }
            }

            return posts;
        }
    }
}
