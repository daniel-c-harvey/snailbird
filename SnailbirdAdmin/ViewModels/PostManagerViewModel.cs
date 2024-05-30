using Microsoft.AspNetCore.Components;
using Core.Converters;
using RazorCore.Navigation;
using RazorCore;
using SnailbirdAdmin.Messages;
using SnailbirdAdmin.Models;
using SnailbirdAdmin.Updates;
using SnailbirdAdmin.Views;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class PostManagerViewModel<TPost, TEdit>
        where TPost : Post, new()
        where TEdit : EditPost<TPost>
    {
        #region "Members"
        protected IDataAdapter<TPost> PostAdapter { get; set; }

        protected PostManagerUpdate<TPost>? update;
        protected PostManagerModel<TPost>? model;
        
        public PostManagerViewModel(IDataAdapter<TPost> postAdapter)
        {
            PostAdapter = postAdapter;
            InitColumnMap();
            InitModel();
            InitNavigation();
        }

        protected void InitModel()
        {
            update = new(PostAdapter);
            model = new(PostManagerMode.View);
            model = update.Update(model, new PostManagerGetPostsMessage(1, 25));
        }


        protected IColumnMap<TPost> columns = default!;
        protected void InitColumnMap()
        {
            columns = new ColumnMap<TPost>()
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
        protected void AddPost(TPost post)
        {
            if (update != null && model != null)
            {
                BeforeModeChange();
                update.Update(model, new PostManagerAddMessage<TPost>(post));
            }
        }

        protected void EditPost(TPost post)
        {
            if (update != null && model != null)
            {
                BeforeModeChange();
                update.Update(model, new PostManagerEditMessage<TPost>(post));
            }
        }

        protected void DeletePost(TPost post)
        {
            if (update != null && model != null)
            {
                update.Update(model, new PostManagerDeleteMessage<TPost>(post));
            }
        }

        protected void SaveNewPost(TPost post)
        {
            if (update != null && model != null)
            {
                BeforeModeChange();
                update.Update(model, new PostManagerSaveNewMessage<TPost>(post));
            }
        }

        protected void SavePost(TPost post)
        {
            if (update != null && model != null)
            {
                BeforeModeChange();
                update.Update(model, new PostManagerSaveExistingMessage<TPost>(post));
            }
        }
        #endregion

        #region "INavigable"
        public INavigator<PostManagerMode> Navigator { get; protected set; }
        protected void InitNavigation()
        {
            Navigator = new Navigator<PostManagerMode, PostManagerModel<TPost>>(model);
        }

        public PostManagerMode CurrentMode => model.CurrentMode;
        protected void BeforeModeChange()
        {
            if (model != null)
            {
                Navigator.OnForward();
            }
        }
        #endregion
    }
}
