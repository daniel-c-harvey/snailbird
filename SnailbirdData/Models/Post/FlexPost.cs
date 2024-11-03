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
    }
}
