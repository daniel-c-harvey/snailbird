using NetBlocks.Models;
using RazorCore.Confirmation;
using RazorCore.Messages;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.Messages
{
    public enum PostManagerAction
    {
        Add,
        Edit,
        Delete,
        SaveNew,
        SaveExisting,
        GetPosts,
        ResetPosts,
    }

    public abstract class PostManagerMessage : MessageBase<PostManagerAction>
    {
        protected PostManagerMessage(PostManagerAction action) : base(action) { }
    }

    public abstract class PostManagerPostMessage<TPost> : PostManagerMessage
        where TPost : Post<TPost>
    {
        public TPost Post { get; }

        public PostManagerPostMessage(PostManagerAction action, TPost post)
        : base(action)
        {
            Post = post;
        }
    }

    public class PostManagerAddMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PromptMessage ConfirmationModel { get; }

        public PostManagerAddMessage(TPost post)
        : base(PostManagerAction.Add, post) 
        {
            ConfirmationModel = new("Adding Post", 
                                    "The post being added has unsaved changes.  " +
                                    "How to proceed?");
        }
    }

    public class PostManagerEditMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PromptMessage ConfirmationModel { get; }

        public PostManagerEditMessage(TPost post)
        : base(PostManagerAction.Edit, post)
        {
            ConfirmationModel = new("Editing Post", 
                                    "The post being edited has unsaved changes.  " +
                                    "How to proceed?");
        }
    }

    public class PostManagerDeleteMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PostManagerDeleteMessage(TPost post)
        : base(PostManagerAction.Delete, post) { }
    }

    public class PostManagerSaveNewMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PostManagerSaveNewMessage(TPost post)
        : base(PostManagerAction.SaveNew, post) { }
    }

    public class PostManagerSaveExistingMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PostManagerSaveExistingMessage(TPost post)
        : base(PostManagerAction.SaveExisting, post) { }
    }

    public class PostManagerGetPostsMessage : PostManagerMessage
    {
        public int Page { get; set; }
        public int PageIndex => Page - 1;
        public int PageSize { get; set; }

        public event MessageEventHandler? NotifyError;

        public PostManagerGetPostsMessage(int page, int pageSize, MessageEventHandler notifyError)
        : base(PostManagerAction.GetPosts)
        {
            Page = page;
            PageSize = pageSize;
            NotifyError += notifyError;
        }

        public void RaiseNotifyError(string message)
        {
            NotifyError?.Invoke(this, new MessageEventArgs(message));
        }
    }

    public class PostManagerResetPostMessage : PostManagerMessage
    {
        public PostManagerResetPostMessage() : base(PostManagerAction.ResetPosts) { }
    }
}
