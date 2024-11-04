using DataAccess;
using SnailbirdData.Models.Post;

namespace SnailbirdData.Models.Entities
{
    public class StudioFeedFlexPost : FlexPost<StudioFeedFlexPost>, IEntity
    {
        public StudioFeedFlexPost() 
        : base()
        { }

        public override StudioFeedFlexPost Clone()
        {
            return new StudioFeedFlexPost()
            {
                ID = ID,
                PostDate = PostDate,
                Title = Title,
                Elements = Elements.Select(x => x.Clone())
            };
        }
    }
}
