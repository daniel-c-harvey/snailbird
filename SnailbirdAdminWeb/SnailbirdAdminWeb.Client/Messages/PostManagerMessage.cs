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

    public abstract class PostManagerNotifyMessage : PostManagerMessage
    {
        public event MessageEventHandler NotifyError;
        protected PostManagerNotifyMessage(PostManagerAction action, MessageEventHandler notifyError) : base(action)
        {
            NotifyError += notifyError;
        }
        
        public void RaiseNotifyError(string message)
        {
            NotifyError.Invoke(this, new MessageEventArgs(message));
        }
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
    
    public abstract class PostManagerNotifyPostMessage<TPost> : PostManagerNotifyMessage
        where TPost : Post<TPost>
    {
        public TPost Post { get; }

        public PostManagerNotifyPostMessage(PostManagerAction action, TPost post, MessageEventHandler notifyError)
        : base(action, notifyError)
        {
            Post = post;
        }
    }

    public class PostManagerAddMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PromptMessage ConfirmationModel { get; }
        public PostManagerSaveNewMessage<TPost> SaveNewMessage { get; }

        public PostManagerAddMessage(TPost post, PostManagerSaveNewMessage<TPost> saveNewMessage)
        : base(PostManagerAction.Add, post) 
        {
            SaveNewMessage = saveNewMessage;
            ConfirmationModel = new PromptMessage("Adding Post", 
                                    "The post being added has unsaved changes.  " +
                                    "How to proceed?");
        }
    }

    public class PostManagerEditMessage<TPost> : PostManagerPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PromptMessage ConfirmationModel { get; }
        public PostManagerSaveExistingMessage<TPost> SaveExistingMessage { get; }

        public PostManagerEditMessage(TPost post, PostManagerSaveExistingMessage<TPost> saveExistingMessage)
        : base(PostManagerAction.Edit, post)
        {
            SaveExistingMessage = saveExistingMessage;
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

    public class PostManagerSaveNewMessage<TPost> : PostManagerNotifyPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PostManagerSaveNewMessage(TPost post, MessageEventHandler notifyError)
        : base(PostManagerAction.SaveNew, post, notifyError) { }
    }

    public class PostManagerSaveExistingMessage<TPost> : PostManagerNotifyPostMessage<TPost>
        where TPost : Post<TPost>
    {
        public PostManagerSaveExistingMessage(TPost post, MessageEventHandler notifyError)
        : base(PostManagerAction.SaveExisting, post, notifyError) { }
    }

    public class PostManagerGetPostsMessage : PostManagerNotifyMessage
    {
        public int Page { get; set; }
        public int PageIndex => Page - 1;
        public int PageSize { get; set; }

        public PostManagerGetPostsMessage(int page, int pageSize, MessageEventHandler notifyError)
        : base(PostManagerAction.GetPosts, notifyError)
        {
            Page = page;
            PageSize = pageSize;
        }
    }

    public class PostManagerResetPostMessage : PostManagerMessage
    {
        public PostManagerResetPostMessage() : base(PostManagerAction.ResetPosts) { }
    }
}
