using Core.Converters;
using DataAccess;
using RazorCore.Navigation;
using RazorCore;
using SnailbirdAdmin.Messages;
using SnailbirdAdmin.Models;
using SnailbirdAdmin.Updates;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class PostManagerViewModel<TPost> : INavigable<PostManagerMode>
        where TPost : Post, new()
    {
        #region "Members"
        public PostManagerModel<TPost> Model { get; set; }
        private PostManagerUpdate<TPost> Update;
        
        public PostManagerViewModel(IDataAdapter<TPost> postAdapter)
        {
            InitColumnMap();

            Model = new();
            Update = new(postAdapter);
            Navigator = new Navigator<PostManagerMode, PostManagerModel<TPost>>(Model);
            
            Model = Update.Update(Model, new PostManagerGetPostsMessage(1, 25), Navigator);
        }

        public IColumnMap<TPost> Columns = default!;
        protected virtual void InitColumnMap()
        {
            Columns = new ColumnMap<TPost>()
                            .AddColumn("ID",
                                new ModelColumn<TPost>(
                                    (p) => LongConverter.ToString(p.ID),
                                    (p, id) => p.ID = LongConverter.FromString(id)))
                            .AddColumn("Title",
                                new ModelColumn<TPost>(
                                    (p) => p.Title,
                                    (p, title) => p.Title = title)
                                .MakeClickable(EditPost))
                            .AddColumn("Date",
                                new ModelColumn<TPost>(
                                    (p) => DateTimeConverter.ToShortDate(p.PostDate),
                                    (p, date) => p.PostDate = DateTimeConverter.FromString(date)));
        }
        #endregion

        #region "Event Handlers"
        public void AddPost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerAddMessage<TPost>(post), Navigator);
            }
        }

        public void EditPost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerEditMessage<TPost>(post), Navigator);
            }
        }

        public void DeletePost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerDeleteMessage<TPost>(post), Navigator);
            }
        }

        public void SaveNewPost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerSaveNewMessage<TPost>(post), Navigator);
            }
        }

        public void SavePost(TPost post)
        {
            if (Update != null && Model != null)
            {
                Update.Update(Model, new PostManagerSaveExistingMessage<TPost>(post), Navigator);
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
