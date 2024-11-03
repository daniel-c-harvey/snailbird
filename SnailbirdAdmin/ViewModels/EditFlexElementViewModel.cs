using NetBlocks;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexElementViewModel
    {
        public event ConfirmEventHandler<string>? ConfirmElementChange;
        public event EventHandler? Ascend;
        public event EventHandler? Descend;
        public event EventHandler? DeleteClicked;

        private FlexElement chosenElement;
        public FlexElement Element
        {
            get => chosenElement;
            protected set
            {
                chosenElement = value.Clone();
            }
        }

        public string SelectedElementName
        {
            get => Element.TypeCaption;
            set
            {
                OnConfirmSelectedElement(value);
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
        }

        public void RaiseAscend()
        {
            Ascend?.Invoke(this, EventArgs.Empty);
        }
        
        public void RaiseDescend()
        {
            Descend?.Invoke(this, EventArgs.Empty);
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

        public void RaiseDeleteClicked()
        {
            DeleteClicked?.Invoke(this, new EventArgs());
        }
    }
}
