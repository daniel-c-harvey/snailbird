using DataAccess;
using NetBlocks.Interfaces;

namespace SnailbirdData.Models.Post
{
    public abstract class Post<TSelf> : IModel, ICloneable<TSelf>
    where TSelf : Post<TSelf>
    {
        public long ID { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime PostDate { get; set; } = default!;

        public abstract TSelf Clone();

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            var other = obj as TSelf;

            return other is not null && 
                   ID == other.ID &&
                   Title == other.Title &&
                   PostDate == other.PostDate;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode() ^ 
                   Title.GetHashCode() ^ 
                   PostDate.GetHashCode();
        }
    }
}
