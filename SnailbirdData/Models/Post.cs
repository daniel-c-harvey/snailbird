using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models
{
    public abstract class Post
    {
        public int Id { get; }
        public string Title { get; }
        public DateTime PostDate { get; }
        //public virtual string Content() { return string.Empty; }

        public Post(int id, string title, DateTime postDate)
        {
            Id = id;
            Title = title;
            PostDate = postDate;
        }

        public string Serialize()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
