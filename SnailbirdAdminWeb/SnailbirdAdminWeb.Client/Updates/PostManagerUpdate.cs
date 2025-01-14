using DataAccess;
using RazorCore.Navigation;
using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.Messages;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.Updates
{
    public class PostManagerUpdate<TPost>
        where TPost : Post<TPost>, new()
    {
        private IPostManagerClient<TPost> PostManager { get; }
        private INavigator<PostManagerMode> Navigator { get; }

        public PostManagerUpdate(IPostManagerClient<TPost> postManager, INavigator<PostManagerMode> navigator)
        {
            PostManager = postManager;
            Navigator = navigator;
        }

        public PostManagerModel<TPost> Update(PostManagerModel<TPost> model,
                                              PostManagerMessage message)
        {
            switch (message.Action)
            {
                case PostManagerAction.Add:
                    if (message is PostManagerAddMessage<TPost> addMessage) AddPost(model, addMessage);
                    break;
                case PostManagerAction.Edit:
                    if (message is PostManagerEditMessage<TPost> editMessage) EditPost(model, editMessage);
                    break;
                case PostManagerAction.Delete:
                    if (message is PostManagerDeleteMessage<TPost> deleteMessage) DeletePost(model, deleteMessage);
                    break;
                case PostManagerAction.SaveNew:
                    if (message is PostManagerSaveNewMessage<TPost> saveNewMessage) SaveNewPost(model, saveNewMessage);
                    break;
                case PostManagerAction.SaveExisting:
                    if (message is PostManagerSaveExistingMessage<TPost> saveMessage) SavePost(model, saveMessage);
                    break;
                case PostManagerAction.GetPosts:
                    if (message is PostManagerGetPostsMessage getMessage) GetPosts(model, getMessage);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return model;
        }

        private void AddPost(PostManagerModel<TPost> model,
                             PostManagerAddMessage<TPost> message)
        {
            // update model with new post
            model.Post = message.Post;
            model.Post.ID = model.Posts.LongCount() + 1;

            // set the Save action if there is a dirty nav
            Navigator.NavigateConfirmationViewModel.Choices[NavigatePromptChoices.Save.Choice] +=
                () => Update(model, new PostManagerSaveNewMessage<TPost>(model.Post));
            Navigator.NavigateConfirmationViewModel.Choices[NavigatePromptChoices.Discard.Choice] +=
                () => ResetPost(model);

            // naviagte to the edit page with the new post and configure the dirty away-navigation prompt
            Navigator.NavigateForward(PostManagerMode.Add)
                     .ConfirmBeforeNavigateAway(message.ConfirmationModel,
                                                (_, args) => args.IsConfirmed = model.IsPostModified);
        }

        private void ResetPost(PostManagerModel<TPost> model)
        {
            model.Post = model.OriginalPost;
        }

        private void EditPost(PostManagerModel<TPost> model,
                              PostManagerEditMessage<TPost> message)
        {
            // update the post to be edited
            model.Post = message.Post.Clone();

            // set the Save action if there is a navigation away from this edit
            Navigator.NavigateConfirmationViewModel.Choices[NavigatePromptChoices.Save.Choice] =
                () => Update(model, new PostManagerSaveExistingMessage<TPost>(model.Post));
            Navigator.NavigateConfirmationViewModel.Choices[NavigatePromptChoices.Discard.Choice] +=
                () => ResetPost(model);

            // navigate to the Edit page and assign the dirty away-navigation prompt
            Navigator.NavigateForward(PostManagerMode.Edit)
                     .ConfirmBeforeNavigateAway(message.ConfirmationModel,
                                                (_, args) => args.IsConfirmed = model.IsPostModified);
        }

        private void DeletePost(PostManagerModel<TPost> model,
                                PostManagerDeleteMessage<TPost> message)
        {
            if (PostManager is not null)
            {
                PostManager.Delete(message.Post);
                model.Posts.Remove(message.Post);
            }
        }

        private void SaveNewPost(PostManagerModel<TPost> model,
                                 PostManagerSaveNewMessage<TPost> message)
        {
            if (PostManager is not null)
            {
                PostManager.Insert(message.Post);
                model.Post = message.Post;
                model.Posts.Add(message.Post);
                Navigator.NavigateBack();
            }
        }

        private void SavePost(PostManagerModel<TPost> model,
                              PostManagerSaveExistingMessage<TPost> message)
        {
            if (PostManager is not null)
            {
                PostManager.Update(message.Post);
                model.Post = message.Post;
                Navigator.NavigateBack();
            }
        }

        private async void GetPosts(PostManagerModel<TPost> model,
                              PostManagerGetPostsMessage message)
        {
            if (PostManager is not null)
            {
                var results = await PostManager.GetPage(message.PageIndex, message.PageSize);
                if (results.Success && results.Value != null)
                {
                    model.Posts = results.Value.ToList();
                    Navigator.NavigateForward(PostManagerMode.View);
                }
            }
        }
    }
}
