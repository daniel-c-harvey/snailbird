using NetBlocks;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexElementViewModel
    {
        public event ConfirmEventHandler<string>? ConfirmElementChange;

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
                OnConfirmSelectedElement(value);
            }
        }

        private void OnConfirmSelectedElement(string value)
        {
            // check for changes in the element before replacing
            if (!Element.Equals(Prototypes.First(p => p.TypeCaption == Element.TypeCaption)))
            {
                ConfirmEventArgs<string> args = new(value);
                ConfirmElementChange?.Invoke(this, args);
            }
        }
        
        public void UpdateSelectedElement(ConfirmEventArgs<string> args)
        {
            if (!args.Confirm)
            {
                // Unconfirmed, abort
                return;
            }
            
            // Proceed in replacing the element
            FlexElement? newPrototype = Prototypes.FirstOrDefault(p => p.TypeCaption == args.NewValue);
            if (newPrototype != null)
            {
                Element = newPrototype;
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
