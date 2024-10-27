using Core;

namespace SnailbirdData.Models.Post
{
    public abstract class FlexPost : Post
    {
        public IEnumerable<FlexElement> Elements { get; set; }

        public FlexPost()
        { 
            Elements = new List<FlexElement>();
        }

        public FlexPost(long ID, string title, DateTime date, IEnumerable<FlexElement> elements)
        {
            this.ID = ID;
            Title = title;
            PostDate = date;
            Elements = Order(elements);
        }

        private static IEnumerable<FlexElement> Order(IEnumerable<FlexElement> elements) 
        {
            foreach (var element in elements.ZipCounted()) 
            {
                element.Entity.Ordinal = element.Ordinal;
            }

            return elements;
        }
    }
}
