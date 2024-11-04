using Core;
using DataAccess;
using RazorCore.Confirmation;
using RazorCore.Navigation;
using SnailbirdAdmin.Messages;
using SnailbirdAdmin.Models;
using SnailbirdData.Models.Post;
using static MongoDB.Driver.WriteConcern;

namespace SnailbirdAdmin.Updates
{
    public class PostManagerUpdate<TPost>
        where TPost : Post, new()
    {
        private IDataAdapter<TPost> PostAdapter { get; }
        private INavigator<PostManagerMode> Navigator { get; }

        public PostManagerUpdate(IDataAdapter<TPost> postAdapter, INavigator<PostManagerMode> navigator)
        {
            PostAdapter = postAdapter;
            Navigator = navigator;
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
            model.Post = message.Post;
            model.Post.ID = (model.Posts?.LongCount() ?? 0) + 1;
            Navigator.NavigateForward(PostManagerMode.Add)
                     .ConfirmBeforeNavigateAway(message.ConfirmationModel);
        }

        private void EditPost(PostManagerModel<TPost> model,
                              PostManagerEditMessage<TPost> message)
        {
            model.Post = message.Post;
            Navigator.NavigateForward(PostManagerMode.Edit)
                     .ConfirmBeforeNavigateAway(message.ConfirmationModel);
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
                Navigator.NavigateBack();
            }
        }

        private void SavePost(PostManagerModel<TPost> model,
                              PostManagerSaveExistingMessage<TPost> message)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Update(message.Post);
                Navigator.NavigateBack();
            }
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
                    Navigator.NavigateForward(PostManagerMode.View);
                }
            }
        }
    }
}
