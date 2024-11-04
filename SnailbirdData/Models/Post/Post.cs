using DataAccess;
using NetBlocks;

namespace SnailbirdData.Models.Post
{
    public abstract class Post : IModel, ICloneable<Post>
    {
        public long ID { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime PostDate { get; set; } = default!;

        public abstract Post Clone();
    }
}
