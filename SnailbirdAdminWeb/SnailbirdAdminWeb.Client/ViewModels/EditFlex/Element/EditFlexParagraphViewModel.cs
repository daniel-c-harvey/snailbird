using NetBlocks.Utilities.Binding;
using RazorCore.Markup;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels.EditFlex.Element
{
    public class EditFlexParagraphViewModel
    {
        private BindingRepo bindings = new();
        public MarkupTextAreaViewModel MarkupViewModel { get; set; }
        public FlexParagraph FlexParagraph { get; set; }

        public EditFlexParagraphViewModel(FlexParagraph paragraph) 
        {
            FlexParagraph   = paragraph;
            MarkupViewModel = new(paragraph.Text);

            bindings.Add(new BindingHelper<string>(MarkupViewModel, 
                                                   () => MarkupViewModel.Text, 
                                                   (value) => MarkupViewModel.Text = value, 
                                                   nameof(MarkupViewModel.Text),
                                                   FlexParagraph, 
                                                   () => FlexParagraph.Text, 
                                                   (value) => FlexParagraph.Text = value, 
                                                   nameof(FlexParagraph.Text)));
        }
    }
}
