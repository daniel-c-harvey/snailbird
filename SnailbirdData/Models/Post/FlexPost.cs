using Core;

namespace SnailbirdData.Models.Post
{
    public abstract class FlexPost : Post
    {
        public IEnumerable<PostElement> Elements { get; set; }

        public FlexPost()
        { 
            Elements = new List<PostElement>();
        }

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
