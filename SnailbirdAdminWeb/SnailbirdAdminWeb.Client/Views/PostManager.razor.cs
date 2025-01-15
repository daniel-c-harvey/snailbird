using Microsoft.AspNetCore.Components;
using NetBlocks.Models;
using RazorCore.Confirmation;
using SnailbirdAdminWeb.Client.Updates;
using SnailbirdData.Models.Post;
using SnailbirdAdminWeb.Client.ViewModels;

namespace SnailbirdAdminWeb.Client.Views
{
    public partial class PostManager<TPost, TEdit, TUpdate>
        where TPost : Post<TPost>, new()
        where TEdit : EditPostViewModelBase<TPost, TEdit>
        where TUpdate : PostManagerUpdate<TPost>
    {
        [Parameter]
        public required PostManagerViewModel<TPost, TEdit, TUpdate> ViewModel { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? AddComponent { get; set; }
        [Parameter]
        public RenderFragment<TEdit>? EditComponent { get; set; }

        private static PromptChoice _okChoice = new(1, "Okay", "btn-outline-primary");
        private static IEnumerable<PromptChoice> _choices = [_okChoice];
        private Prompt? Prompt { get; set; }
        private PromptViewModel _promptViewModel = new(_choices);

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (ViewModel is null) return;
            
            ViewModel.Navigator.ModeChanged += (_) => ModeChanged();
            ViewModel.NotifyError += OpenPrompt;
        }

        private void ModeChanged()
        {
            StateHasChanged();
        }
        
        private void OpenPrompt(object sender, MessageEventArgs e)
        {
            _promptViewModel.PromptMessage = new PromptMessage("Error",e.Message);
            Prompt?.Open();
        }
    }
}
