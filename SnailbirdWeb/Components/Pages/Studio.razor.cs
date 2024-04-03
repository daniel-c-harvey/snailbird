using SnailbirdData.Providers;
using Microsoft.AspNetCore.Components;

namespace SnailbirdWeb.Components.Pages
{
    public partial class Studio
    {
        [Inject]
        private IPostProvider postProvider { get; set; } = default!;
        public SnailbirdData.Models.LiveJamPost Post { get; set; } = default!;

        protected override void OnInitialized()
        {
            Post = postProvider.GetPost<SnailbirdData.Models.LiveJamPost>(7);
        }
    }
}
