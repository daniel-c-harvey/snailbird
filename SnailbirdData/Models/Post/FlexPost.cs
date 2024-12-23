using SnailbirdData.Models.Entities;

namespace SnailbirdData.Models.Post
{
    public abstract class FlexPost<TPost> : Post<TPost>
    where TPost : FlexPost<TPost>, new()
    {
        public IEnumerable<FlexElement> Elements { get; set; }

        public FlexPost()
        { 
            Elements = new List<FlexElement>();
        }

        public override TPost Clone()
        {
            return new TPost()
            {
                ID = ID,
                Title = Title,
                PostDate = PostDate,
                Elements = Elements.Select(e => e.Clone()).ToList()
            };
        }

        public override bool Equals(object? obj)
        {
            var other = obj as FlexPost<TPost>;
            return base.Equals(other) && Elements.SequenceEqual(other.Elements);
        }

        public override int GetHashCode()
        {
            return Elements.Aggregate(base.GetHashCode(), (sofar, next) => sofar ^ next.GetHashCode());
        }
    }
}
