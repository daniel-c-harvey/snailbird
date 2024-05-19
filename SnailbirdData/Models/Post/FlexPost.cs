using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models.Post
{
    public class FlexPost : Post
    {
        public IEnumerable<PostElement> Elements { get; set; }

        public FlexPost(long ID, string title, DateTime date, IEnumerable<PostElement> elements)
        {
            this.ID = ID;
            Title = title;
            PostDate = date;
            Elements = Order(elements);
        }

        private static IEnumerable<PostElement> Order(IEnumerable<PostElement> elements) 
        {
            foreach (var element in elements.ZipCounted()) 
            {
                element.Entity.Ordinal = element.Ordinal;
            }

            return elements;
        }
    }
}
