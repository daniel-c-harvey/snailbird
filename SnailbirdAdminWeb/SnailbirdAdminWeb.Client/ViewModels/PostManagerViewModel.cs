using NetBlocks.Models;
using RazorCore.Navigation;
using SnailbirdAdminWeb.Client.Messages;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdAdminWeb.Client.Updates;
using SnailbirdData.Models.Post;
using RazorCore.Table;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public class PostManagerViewModel<TPost, TEdit, TUpdate> : INavigable<PostManagerMode>
        where TPost : Post<TPost>, new()
        where TEdit : EditPostViewModelBase<TPost, TEdit>
        where TUpdate : PostManagerUpdate<TPost>
    {
        #region "Members"
        public IColumnMap<TPost>? Columns { get; private set; }
        public PostManagerModel<TPost> Model { get; }
        public event MessageEventHandler? NotifyError;
        private readonly PostManagerUpdate<TPost> _update;

        public EditPostViewModelBase<TPost, TEdit>? EditingViewModel { get; set; }
        public EditPostViewModelBase<TPost, TEdit>? AddViewModel { get; set; }

        public PostManagerViewModel(PostManagerModel<TPost> model,
                                    Navigator<PostManagerMode, PostManagerModel<TPost>> navigator,
                                    TUpdate update)
        {
            _update = update;
            Navigator = navigator;
            Model = _update.Update(model, new PostManagerGetPostsMessage(1, 25, RaiseNotifyError));
        }

        public virtual void InitColumnMap()
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
            _update.Update(Model, new PostManagerAddMessage<TPost>(post, 
                new PostManagerSaveNewMessage<TPost>(post, RaiseNotifyError)));
        }

        public void EditPost(TPost post)
        {
            _update.Update(Model, new PostManagerEditMessage<TPost>(post, 
                new PostManagerSaveExistingMessage<TPost>(post, RaiseNotifyError)));
        }

        public void DeletePost(TPost post)
        {
            _update.Update(Model, new PostManagerDeleteMessage<TPost>(post));
        }

        public void SaveNewPost(TPost post)
        {
            _update.Update(Model, new PostManagerSaveNewMessage<TPost>(post, RaiseNotifyError));
        }

        public void SavePost(TPost post)
        {
            _update.Update(Model, new PostManagerSaveExistingMessage<TPost>(post, RaiseNotifyError));
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
        
        private void RaiseNotifyError(object sender, MessageEventArgs e)
        {
            NotifyError?.Invoke(this, new MessageEventArgs(e.Message));
        }
    }
}
