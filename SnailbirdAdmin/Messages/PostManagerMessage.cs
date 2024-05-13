using SnailbirdData.Models;
using SnailbirdData.DataAdapters;

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

    public class PostManagerAddMessage : PostManagerMessage
    {
        public LiveJamPost NewPost { get; }

        public PostManagerAddMessage(LiveJamPost newPost)
        : base(PostManagerAction.Add)
        {
            NewPost = newPost;
        }
    }

    public abstract class PostManagerPostMessage : PostManagerMessage
    {
        public LiveJamPost Post { get; }

        public PostManagerPostMessage(PostManagerAction action, LiveJamPost post)
        : base(action)
        {
            Post = post;
        }
    }

    public class PostManagerEditMessage : PostManagerPostMessage
    {
        public PostManagerEditMessage(LiveJamPost post)
        : base(PostManagerAction.Edit, post) { }
    }

    public class PostManagerDeleteMessage : PostManagerPostMessage
    {
        public PostManagerDeleteMessage(LiveJamPost post)
        : base(PostManagerAction.Delete, post) { }
    }

    public class PostManagerSaveNewMessage : PostManagerPostMessage
    {
        public PostManagerSaveNewMessage(LiveJamPost post)
        : base(PostManagerAction.SaveNew, post) { }
    }

    public class PostManagerSaveExistingMessage : PostManagerPostMessage
    {
        public PostManagerSaveExistingMessage(LiveJamPost post)
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
