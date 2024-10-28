using DataAccess;
using SnailbirdData.Models.Post;

namespace SnailbirdData.Models.Entities
{
    public class StudioFeedFlexPost : FlexPost, IEntity
    {
        public StudioFeedFlexPost() 
        : base()
        { }

        public StudioFeedFlexPost(long ID, string title, DateTime date, IEnumerable<FlexElement> elements)
        : base(ID, title, date, elements)
        {}
    }
}
