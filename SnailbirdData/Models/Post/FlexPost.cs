namespace SnailbirdData.Models.Post
{
    public abstract class FlexPost<TPost> : Post<TPost>
    where TPost : Post<TPost>
    {
        public IEnumerable<FlexElement> Elements { get; set; }

        public FlexPost()
        { 
            Elements = new List<FlexElement>();
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
