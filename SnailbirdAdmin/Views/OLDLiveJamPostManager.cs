using Microsoft.AspNetCore.Components;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Views
{
    public class LiveJamPostManager : PostManagerBase<LiveJamPost, EditLiveJamPost>
    {
        protected override RenderFragment AddComponent => builder =>
        {
            builder.OpenComponent(0, typeof(EditLiveJamPost));
            builder.AddAttribute(1, nameof(EditLiveJamPost.Post), model?.Post);
            builder.AddAttribute(2, nameof(EditLiveJamPost.OnCommitPost), SaveNewPost);
            builder.CloseComponent();
        };

        protected override RenderFragment EditComponent => builder =>
        {
            builder.OpenComponent(0, typeof(EditLiveJamPost));
            builder.AddAttribute(1, nameof(EditLiveJamPost.Post), model?.Post);
            builder.AddAttribute(2, nameof(EditLiveJamPost.OnCommitPost), SavePost);
            builder.CloseComponent();
        };
    }
}
