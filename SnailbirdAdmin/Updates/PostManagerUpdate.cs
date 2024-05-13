using Microsoft.AspNetCore.Components;
using SnailbirdAdmin.Messages;
using SnailbirdAdmin.Models;
using SnailbirdData.Models;
using SnailbirdData.DataAdapters;

namespace SnailbirdAdmin.Updates
{
    public class PostManagerUpdate
    {
        private IDataAdapter<LiveJamPost> PostAdapter { get; }

        public PostManagerUpdate(IDataAdapter<LiveJamPost> postAdapter)
        {
            PostAdapter = postAdapter;
        }

        public PostManagerModel Update(PostManagerModel model,
                                              PostManagerMessage message)
        {
            switch (message.Action)
            {
                case PostManagerAction.Add:
                    var addMessage = message as PostManagerAddMessage;
                    if (addMessage is not null) AddPost(model, addMessage);
                    break;
                case PostManagerAction.Edit:
                    var editMessage = message as PostManagerEditMessage;
                    if (editMessage is not null) EditPost(model, editMessage);
                    break;
                case PostManagerAction.Delete:
                    var deleteMessage = message as PostManagerDeleteMessage;
                    if (deleteMessage is not null) DeletePost(model, deleteMessage);
                    break;
                case PostManagerAction.SaveNew:
                    var saveNewMessage = message as PostManagerSaveNewMessage;
                    if (saveNewMessage is not null) SaveNewPost(model, saveNewMessage);
                    break;
                case PostManagerAction.SaveExisting:
                    var saveMessage = message as PostManagerSaveExistingMessage;
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

        private void AddPost(PostManagerModel model,
                                    PostManagerAddMessage message)
        {
            model.Post = message.NewPost;
            model.Post.ID = (model.Posts?.LongCount() ?? 0) + 1;
            model.CurrentMode = PostManagerMode.Add;
        }

        private void EditPost(PostManagerModel model,
                                     PostManagerEditMessage message)
        {
            model.Post = message.Post;
            model.CurrentMode = PostManagerMode.Edit;
        }

        private void DeletePost(PostManagerModel model,
                                       PostManagerDeleteMessage message)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Delete(message.Post);
            }
        }

        private void SaveNewPost(PostManagerModel model,
                                        PostManagerSaveNewMessage message)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Insert(message.Post);
            }
            model.CurrentMode = PostManagerMode.View;
        }

        private void SavePost(PostManagerModel model,
                                     PostManagerSaveExistingMessage message)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Update(message.Post);
            }
            model.CurrentMode = PostManagerMode.View;
        }

        private void GetPosts(PostManagerModel model,
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
