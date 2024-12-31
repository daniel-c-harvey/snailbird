using RazorCore.Navigation;
using SnailbirdAdminWeb.Client.Messages;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdAdminWeb.Client.Updates;
using SnailbirdData.Models.Post;
using SnailbirdAdminWeb.Client.API;
using RazorCore.Table;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public class PostManagerViewModel<TPost, TEdit> : INavigable<PostManagerMode>
        where TPost : Post<TPost>, new()
        where TEdit : EditPostViewModelBase<TPost, TEdit>
    {
        #region "Members"
        public PostManagerModel<TPost> Model { get; set; }
        private PostManagerUpdate<TPost> Update;

        public EditPostViewModelBase<TPost, TEdit>? EditingViewModel { get; set; }
        public EditPostViewModelBase<TPost, TEdit>? AddViewModel { get; set; }

        public PostManagerViewModel(IPostManagerClient<TPost> postManager)
        {
            InitColumnMap();

            Model = new();
            Navigator = new Navigator<PostManagerMode, PostManagerModel<TPost>>(Model);
            Update = new(postManager, Navigator);
            
            Model = Update.Update(Model, new PostManagerGetPostsMessage(1, 25));
        }

        public IColumnMap<TPost> Columns = default!;
        protected virtual void InitColumnMap()
        {
            Columns = new ColumnMap<TPost>()
                            .AddColumn(
                                ColumnKey.Init(typeof(TPost).GetProperty(nameof(Post<TPost>.ID))),
                                new ModelColumn<TPost, long>(
                                    (p) => p.ID,
                                    (p, id) => p.ID = id))
                            .AddColumn(
                                ColumnKey.Init(typeof(TPost).GetProperty(nameof(Post<TPost>.Title))),
                                new ModelColumn<TPost, string>(
                                    (p) => p.Title,
                                    (p, title) => p.Title = title)
                                .WithClickable(EditPost))
                            .AddColumn(
                                ColumnKey.Init("Date", typeof(TPost).GetProperty(nameof(Post<TPost>.PostDate))),
                                new ModelColumn<TPost, DateTime>(
                                    (p) => p.PostDate,
                                    (p, date) => p.PostDate = date));
        }
        #endregion

        #region "Event Handlers"
        public void AddPost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerAddMessage<TPost>(post));
            }
        }

        public void EditPost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerEditMessage<TPost>(post));
            }
        }

        public void DeletePost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerDeleteMessage<TPost>(post));
            }
        }

        public void SaveNewPost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerSaveNewMessage<TPost>(post));
            }
        }

        public void SavePost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerSaveExistingMessage<TPost>(post));
            }
        }
        #endregion

        #region "INavigable"
        public INavigator<PostManagerMode> Navigator { get; protected set; }

        public PostManagerMode CurrentMode {
            get
            {
                if (Model is null) throw new ArgumentNullException(nameof(Model));
                return Model.CurrentMode;
            }
        }
        #endregion
    }
}
