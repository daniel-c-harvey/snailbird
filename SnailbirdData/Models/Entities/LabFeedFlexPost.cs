using DataAccess;
using SnailbirdData.Models.Post;

namespace SnailbirdData.Models.Entities
{
    public class LabFeedFlexPost : FlexPost<LabFeedFlexPost>, IEntity
    {
        public LabFeedFlexPost()
        : base()
        { }

        public override LabFeedFlexPost Clone()
        {
            return new LabFeedFlexPost()
            {
                ID = ID,
                Title = Title,
                PostDate = PostDate,
                Elements = Elements.Select(e => e.Clone()).ToList()
            };
        }

        public override int GetHashCode()
        {
            return Elements.Aggregate(base.GetHashCode(), (sofar, next) => sofar ^ next.GetHashCode());
        }
    }
}
