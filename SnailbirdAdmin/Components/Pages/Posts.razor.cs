using Microsoft.AspNetCore.Components;
using Core.Converters;
using RazorCore;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models;

namespace SnailbirdAdmin.Components.Pages
{
    public partial class Posts
    {
        [Inject]
        IDataAdapter<LiveJamPost> PostAdapter { get; set; } = default!;

        protected enum Mode
        {
            View,
            Add,
            Edit
        }

        private IColumnMap<LiveJamPost> columns = default!;

        private Mode _currentMode = Mode.View;
        protected Mode CurrentMode
        {
            get { return _currentMode; }
            set
            {
                _currentMode = value;
                StateHasChanged();
            }
        }
        private LiveJamPost? Post = null;
        private IEnumerable<LiveJamPost>? _posts = null;

        protected override void OnInitialized()
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

            var results = PostAdapter.GetPage(0, 25);
            if (results.Success)
            {
                _posts = results.Value;
            }
        }

        protected void AddPost(LiveJamPost post)
        {
            Post = post;
            Post.ID = _posts.Count();
            CurrentMode = Mode.Add;
        }

        protected void EditPost(LiveJamPost post)
        {
            Post = post;
            CurrentMode = Mode.Edit;
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
