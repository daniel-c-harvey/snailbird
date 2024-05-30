using Microsoft.AspNetCore.Components;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.Views
{
    public class FlexPostManager : PostManagerBase<FlexPost, EditFlexPost>
    {
        protected override RenderFragment AddComponent => builder =>
        {
            builder.OpenComponent(0, typeof(EditFlexPost));
            builder.AddAttribute(1, nameof(EditFlexPost.Post), model?.Post);
            builder.AddAttribute(2, nameof(EditFlexPost.OnCommitPost), SaveNewPost);
            builder.CloseComponent();
        };

        protected override RenderFragment EditComponent => builder =>
        {
            builder.OpenComponent(0, typeof(EditFlexPost));
            builder.AddAttribute(1, nameof(EditFlexPost.Post), model?.Post);
            builder.AddAttribute(2, nameof(EditFlexPost.OnCommitPost), SavePost);
            builder.CloseComponent();
        };
    }
}
