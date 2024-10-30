using NetBlocks;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexElementViewModel
    {
        private FlexElement chosenElement;
        public FlexElement Element
        {
            get => chosenElement;
            protected set
            {
                chosenElement = value.Clone();
                chosenElement.Ordinal = Ordinal;
            }
        }

        private int Ordinal {  get; set; }

        public string SelectedElementName
        {
            get => Element.TypeCaption;
            set
            {
                FlexElement? prototype = Prototypes.FirstOrDefault(p => p.TypeCaption == value);
                if (prototype != null)
                {
                    Element = prototype;
                }
            }
        }

        public static IEnumerable<FlexElement> Prototypes { get; }

        static EditFlexElementViewModel()
        {
            Prototypes = Prototyper.PrototypeDerivedTypes<FlexElement>();
        }

        public EditFlexElementViewModel(FlexElement element)
        {
            chosenElement = element;
            Ordinal = element.Ordinal;
        }
    }
}
