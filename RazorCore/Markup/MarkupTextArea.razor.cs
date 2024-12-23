using Microsoft.AspNetCore.Components;

namespace RazorCore.Markup
{
    public partial class MarkupTextArea
    {
        [Parameter]
        public required MarkupTextAreaViewModel ViewModel { get; set; }

        [Parameter]
        public bool PreviewMarkup { get; set; } = false;
    }
}
