using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Messages
{
    public enum PostManagerAction
    {
        Add,
        Edit,
        Delete,
        SaveNew,
        SaveExisting,
        GetPosts,
    }

    public abstract class PostManagerMessage : MessageBase<PostManagerAction>
    {
        protected PostManagerMessage(PostManagerAction action) : base(action) { }
    }

    public class PostManagerAddMessage<TPost> : PostManagerMessage
        where TPost : Post
    {
        public TPost NewPost { get; }

        public PostManagerAddMessage(TPost newPost)
        : base(PostManagerAction.Add)
        {
            NewPost = newPost;
        }
    }

    public abstract class PostManagerPostMessage<TPost> : PostManagerMessage
        where TPost : Post
    {
        public TPost Post { get; }

        public PostManagerPostMessage(PostManagerAction action, TPost post)
        : base(action)
        {
            Post = post;
        }
    }

    public class PostManagerEditMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post
    {
        public PostManagerEditMessage(TPost post)
        : base(PostManagerAction.Edit, post) { }
    }

    public class PostManagerDeleteMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post
    {
        public PostManagerDeleteMessage(TPost post)
        : base(PostManagerAction.Delete, post) { }
    }

    public class PostManagerSaveNewMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post
    {
        public PostManagerSaveNewMessage(TPost post)
        : base(PostManagerAction.SaveNew, post) { }
    }

    public class PostManagerSaveExistingMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post
    {
        public PostManagerSaveExistingMessage(TPost post)
        : base(PostManagerAction.SaveExisting, post) { }
    }

    public class PostManagerGetPostsMessage : PostManagerMessage
    {
        public int Page { get; set; }
        public int PageIndex => Page - 1;
        public int PageSize { get; set; }

        public PostManagerGetPostsMessage(int page, int pageSize)
        : base(PostManagerAction.GetPosts)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
