using Microsoft.AspNetCore.Components;
using Core.Converters;
using RazorCore;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;
using SnailbirdAdmin.Updates;
using Microsoft.AspNetCore.Components.Routing;
using SnailbirdAdmin.Models;
using SnailbirdAdmin.Messages;
using RazorCore.Navigation;

namespace SnailbirdAdmin.Views
{

    public partial class PostManager<TPost> : INavigable<PostManagerMode>
        where TPost : Post, new()
    {
        #region "Members"
        [Inject]
        public IDataAdapter<TPost> PostAdapter { protected get; set; } = default!;

        //[Inject]
        //NavigationManager? NavigationManager { get; set; }

        protected PostManagerUpdate<TPost>? update;
        protected PostManagerModel<TPost>? model;
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
        protected override void OnInitialized()
        {
            InitColumnMap();
            InitModel();
            InitNavigation();
        }

        //protected override void OnAfterRender(bool firstRender)
        //{
        //    base.OnAfterRender(firstRender);
        //    if (firstRender)
        //    {
        //        BeforeModeChange();
        //    }
        //}
        #endregion


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

        #region "INavigable"
        public INavigator<PostManagerMode> Navigator { get; protected set; }
        protected void InitNavigation()
        {
            Navigator = new Navigator<PostManagerMode, PostManagerModel<TPost>>(model); 
            Navigator.ModeChanging += (_) => ModeChanged();
        }

        public PostManagerMode CurrentMode => model.CurrentMode;
        protected void BeforeModeChange()
        {
            if (model != null)
            {
                Navigator.OnForward();
            }
        }

        public void ModeChanged()
        {
            StateHasChanged();
        }
        #endregion
    }
}
