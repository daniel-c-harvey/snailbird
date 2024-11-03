using Core;
using RazorCore.Navigation;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Models
{
    public class PostManagerMode : Enumeration<PostManagerMode>
    {
        public static PostManagerMode View = new(1, nameof(View));
        public static PostManagerMode Add = new(2, nameof(Add));
        public static PostManagerMode Edit = new(3, nameof(Edit));

        public PostManagerMode(int id, string name) : base(id, name) { }
    }

    public class PostManagerModel<TPost> : IMode<PostManagerMode>
        where TPost : Post, new()
    {
        public IEnumerable<TPost> Posts { get; set; }
        public TPost Post { get; set; }
        public PostManagerMode CurrentMode { get; set; } = default!;

        public PostManagerModel()
        {
            Posts = new List<TPost>();
            Post = new TPost();
        }

        public PostManagerModel(IEnumerable<TPost> posts, TPost post)
        {
            Posts = posts;
            Post = post;
        }

        public TPost CurrentPost()
        {
            return Post;
        }
    }
}
