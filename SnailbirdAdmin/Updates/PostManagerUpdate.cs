using DataAccess;
using SnailbirdAdmin.Messages;
using SnailbirdAdmin.Models;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Updates
{
    public class PostManagerUpdate<TPost>
        where TPost : Post, new()
    {
        private IDataAdapter<TPost> PostAdapter { get; }

        public PostManagerUpdate(IDataAdapter<TPost> postAdapter)
        {
            PostAdapter = postAdapter;
        }

        public PostManagerModel<TPost> Update(PostManagerModel<TPost> model,
                                              PostManagerMessage message)
        {
            switch (message.Action)
            {
                case PostManagerAction.Add:
                    var addMessage = message as PostManagerAddMessage<TPost>;
                    if (addMessage is not null) AddPost(model, addMessage);
                    break;
                case PostManagerAction.Edit:
                    var editMessage = message as PostManagerEditMessage<TPost>;
                    if (editMessage is not null) EditPost(model, editMessage);
                    break;
                case PostManagerAction.Delete:
                    var deleteMessage = message as PostManagerDeleteMessage<TPost>;
                    if (deleteMessage is not null) DeletePost(model, deleteMessage);
                    break;
                case PostManagerAction.SaveNew:
                    var saveNewMessage = message as PostManagerSaveNewMessage<TPost>;
                    if (saveNewMessage is not null) SaveNewPost(model, saveNewMessage);
                    break;
                case PostManagerAction.SaveExisting:
                    var saveMessage = message as PostManagerSaveExistingMessage<TPost>;
                    if (saveMessage  is not null) SavePost(model, saveMessage);
                    break;
                case PostManagerAction.GetPosts:
                    var getMessage = message as PostManagerGetPostsMessage;
                    if (getMessage is not null) GetPosts(model, getMessage);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return model;
        }

        private void AddPost(PostManagerModel<TPost> model,
                             PostManagerAddMessage<TPost> message)
        {
            model.Post = message.NewPost;
            model.Post.ID = (model.Posts?.LongCount() ?? 0) + 1;
            model.CurrentMode = PostManagerMode.Add;
        }

        private void EditPost(PostManagerModel<TPost> model,
                              PostManagerEditMessage<TPost> message)
        {
            model.Post = message.Post;
            model.CurrentMode = PostManagerMode.Edit;
        }

        private void DeletePost(PostManagerModel<TPost> model,
                                PostManagerDeleteMessage<TPost> message)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Delete(message.Post);
            }
        }

        private void SaveNewPost(PostManagerModel<TPost> model,
                                 PostManagerSaveNewMessage<TPost> message)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Insert(message.Post);
            }
            model.CurrentMode = PostManagerMode.View;
        }

        private void SavePost(PostManagerModel<TPost> model,
                              PostManagerSaveExistingMessage<TPost> message)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Update(message.Post);
            }
            model.CurrentMode = PostManagerMode.View;
        }

        private void GetPosts(PostManagerModel<TPost> model,
                              PostManagerGetPostsMessage message)
        {
            if (PostAdapter is not null)
            {
                var results = PostAdapter.GetPage(message.PageIndex, message.PageSize);
                if (results.Success)
                {
                    model.Posts = results.Value;
                }
            }
        }
    }
}
