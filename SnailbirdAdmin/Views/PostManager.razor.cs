using Microsoft.AspNetCore.Components;
using Core.Converters;
using RazorCore;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models;
using SnailbirdAdmin.Updates;
using Microsoft.AspNetCore.Components.Routing;
using SnailbirdAdmin.Models;
using SnailbirdAdmin.Messages;

namespace SnailbirdAdmin.Views
{

    public partial class PostManager : INavigable<PostManagerMode>
    {
        #region "Members"
        [Inject]
        public IDataAdapter<LiveJamPost> PostAdapter { private get; set; } = default!;

        //[Inject]
        //NavigationManager? NavigationManager { get; set; }

        private PostManagerUpdate? update;
        private PostManagerModel? model;
        private void InitModel()
        {
            update = new(PostAdapter);
            model = new(PostManagerMode.View);
            model = update.Update(model, new PostManagerGetPostsMessage(1, 25));
        }
        

        private IColumnMap<LiveJamPost> columns = default!;
        private void InitColumnMap()
        {
            columns = new ColumnMap<LiveJamPost>()
                            .AddColumn("ID",
                                new ModelColumn<LiveJamPost>(
                                    (p) => LongConverter.ToString(p.ID),
                                    (p, id) => p.ID = LongConverter.FromString(id)))
                            .AddColumn("Title",
                                new ModelColumn<LiveJamPost>(
                                    (p) => p.Title,
                                    (p, title) => p.Title = title)
                                .MakeClickable(EditPost))
                            .AddColumn("Date",
                                new ModelColumn<LiveJamPost>(
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


        protected void AddPost(LiveJamPost post)
        {
            if (update != null && model != null)
            {
                BeforeModeChange();
                update.Update(model, new PostManagerAddMessage(post));
            }
        }

        protected void EditPost(LiveJamPost post)
        {
            if (update != null && model != null)
            {
                BeforeModeChange();
                update.Update(model, new PostManagerEditMessage(post));
            }
        }

        protected void DeletePost(LiveJamPost post)
        {
            if (update != null && model != null)
            {
                update.Update(model, new PostManagerDeleteMessage(post));
            }
        }

        protected void SaveNewPost(LiveJamPost post)
        {
            if (update != null && model != null)
            {
                BeforeModeChange();
                update.Update(model, new PostManagerSaveNewMessage(post));
            }
        }

        protected void SavePost(LiveJamPost post)
        {
            if (update != null && model != null)
            {
                BeforeModeChange();
                update.Update(model, new PostManagerSaveExistingMessage(post));
            }
        }

        #region "INavigable"
        private void InitNavigation()
        {
            ModeChanging += (_) => StateHasChanged();
        }

        public PostManagerMode CurrentMode => model.CurrentMode;

        public event ModeChangeEventHandler<PostManagerMode>? ModeChanging;
        private void BeforeModeChange()
        {
            if (model != null && ModeChanging != null)
            {
                ModeChanging(new ModeChangeEventArgs<PostManagerMode>(model.CurrentMode));
            }
        }

        public void OnBack(PostManagerMode mode)
        {
            if (model != null)
            {
                model.CurrentMode = mode;
                StateHasChanged();
            }
        }
        #endregion
    }
}
