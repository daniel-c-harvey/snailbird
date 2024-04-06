using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SnailbirdData.Providers
{
    public class PostEmbeddedResourceProvider<TPost> : IPostProvider<TPost>
        where TPost : Models.Post
    {
        public TPost GetPost(int id)
        {
            string json = Core.FileLoader.LoadResourceFileAsString(Assembly.GetExecutingAssembly(), 
                                                                   $"SnailbirdData.Data.Posts.post{id}.json");
            
            TPost? obj = JsonConvert.DeserializeObject<TPost>(json);
            return obj ?? throw new Exception();
        }

        public IEnumerable<TPost> GetRecentPosts(int pageIndex, int pageLength)
        {
            IEnumerable<string> postNames = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where((resName) => resName.StartsWith("SnailbirdData.Data.Posts."));
            var posts = new List<TPost>(postNames.Count());
            
            foreach(string name in postNames)
            {
                Match matchID = Regex.Match(name, @"SnailbirdData[.]Data[.]Posts[.]post(?<ID>\d+)[.]json");
                if (matchID.Groups.ContainsKey("ID") && int.TryParse(matchID.Groups["ID"].Value, out int postID))
                {
                    posts.Add(GetPost(postID));
                }
            }

            return posts;
        }
    }
}
