using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models
{
    public class SimplePost : Post
    {
        private string content;

        //public override string Content()
        //{
        //    return content;
        //}

        public SimplePost(int id, string title, DateTime postDate, string content) 
            : base(id, title, postDate)
        {
            this.content = content;
        }
    }
}
