using NetBlocks.Utilities;
using RazorCore.Navigation;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.Models
{
    public class PostManagerMode : Enumeration<PostManagerMode>
    {
        public static PostManagerMode View = new(1, nameof(View));
        public static PostManagerMode Add = new(2, nameof(Add));
        public static PostManagerMode Edit = new(3, nameof(Edit));

        public PostManagerMode(int id, string name) : base(id, name) { }
    }

    public class PostManagerModel<TPost> : IMode<PostManagerMode>
        where TPost : Post<TPost>, new()
    {
        public IList<TPost> Posts { get; set; }

        private TPost _post = default!;
        private TPost _originalPost = default!;
        public TPost Post 
        {
            get => _post;
            set
            {
                _post = value;
                _originalPost = _post.Clone();
                
                var z = Posts.FirstOrDefault(p => p.ID == _post.ID);
                if (z != null)
                {
                    Posts[Posts.IndexOf(z)] = _post;
                }
            }
        }

        public TPost OriginalPost => _originalPost;
        public PostManagerMode CurrentMode { get; set; } = default!;
        public bool IsPostModified => !Post.Equals(OriginalPost);

        public PostManagerModel()
        {
            Posts = new List<TPost>();
            Post = new TPost();
        }
    }
}
