using SnailbirdData.DataAdapters;
using SnailbirdData.Models;
using RazorCore;
using Core.Converters;
using Microsoft.AspNetCore.Components;

namespace SnailbirdAdmin.Components.Pages
{
    public partial class Posts
    {
        [Inject]
        IDataAdapter<LiveJamPost> PostAdapter { get; set; } = default!;

        private enum Mode
        {
            View,
            Add,
            Edit
        }

        private static IColumnMap<LiveJamPost> columns = new ColumnMap<LiveJamPost>()
            .AddColumn("ID",
                new ModelColumn<LiveJamPost>(
                    (p) => IntConverter.ToString(p.ID),
                    (p, id) => p.ID = IntConverter.FromString(id),
                    editable: true))
            .AddColumn("Title",
                new ModelColumn<LiveJamPost>(
                    (p) => p.Title,
                    (p, title) => p.Title = title,
                    editable: true));

        private Mode _currentMode = Mode.View;
        private Mode CurrentMode
        {
            get { return _currentMode; }
            set
            {
                _currentMode = value;
                StateHasChanged();
            }
        }
        private LiveJamPost? NewPost = null;
        private IEnumerable<SnailbirdData.Models.LiveJamPost> _posts = new SnailbirdData.Models.LiveJamPost[] { };

        protected override void OnInitialized()
        {
            var results = PostAdapter.GetPage(0, 25);
            if (results.Success)
            {
                _posts = results.Value;
            }
        }

        protected void AddPost(LiveJamPost post)
        {
            NewPost = post;
            NewPost.ID = _posts.Count();
            CurrentMode = Mode.Add;
            //StateHasChanged();
        }

        protected void DeletePost(LiveJamPost post)
        {
            var results = PostAdapter.Delete(post);
        }

        protected void SaveNewPost(LiveJamPost post)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Insert(post);
            }
            CurrentMode = Mode.View;
        }

        protected void SavePost(LiveJamPost post)
        {
            if (PostAdapter is not null)
            {
                PostAdapter.Update(post);
            }
        }
    }
}
