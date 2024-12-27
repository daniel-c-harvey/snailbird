using Microsoft.AspNetCore.Components;

namespace RazorCore.Confirmation
{
    public partial class Prompt
    {
        [Parameter]
        public required PromptViewModel ViewModel { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        public void Open()
        {
            IsVisible = true;
            StateHasChanged();
        }

        private void Close(PromptChoice choice)
        {
            IsVisible = false;
            StateHasChanged();
            ViewModel.Choices[choice]?.Invoke();
            ClearChoiceHandlers();
        }

        private void ClearChoiceHandlers()
        {
            foreach (var choice in ViewModel.Choices.Keys)
            {
                ViewModel.Choices[choice] = null;
            }
        }
    }
}
