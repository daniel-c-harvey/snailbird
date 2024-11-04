﻿using Core;
using NetBlocks;
using RazorCore.Confirmation;
using SnailbirdData.Models.Post;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexElementViewModel
    {
        public event ConfirmEventHandler? ConfirmElementChange;
        public event EventHandler? ElementChanged;
        public event EventHandler? Ascend;
        public event EventHandler? Descend;
        public event EventHandler? DeleteClicked;
        public PromptViewModel ConfirmationViewModel { get; }

        private FlexElement chosenElement;
        public FlexElement Element
        {
            get => chosenElement;
            protected set
            {
                chosenElement = value.Clone();
            }
        }

        private string? _newElementName;
        public string SelectedElementName
        {
            get => Element.TypeCaption;
            set
            {
                OnSetSelectedElement(value);
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
            ConfirmationViewModel = new();
        }

        public void RaiseAscend()
        {
            Ascend?.Invoke(this, EventArgs.Empty);
        }
        
        public void RaiseDescend()
        {
            Descend?.Invoke(this, EventArgs.Empty);
        }
        private void OnSetSelectedElement(string value)
        {
            _newElementName = value;
            // check for changes in the element before replacing
            if (!Element.Equals(Prototypes.First(p => p.TypeCaption == Element.TypeCaption)))
            {
                ConfirmationViewModel.Prompt = new("Confirm Element Replacement",
                                                  "The contents of this element will be reset and replaced with a new blank element. Proceed?");
                ConfirmationViewModel.OnClose = UpdateSelectedElement;
                ConfirmElementChange?.Invoke(this, new ConfirmEventArgs());
            }
            else
            {
                // no changes to confirm, go ahead
                UpdateSelectedElement(new ConfirmEventArgs() { IsConfirmed = true });
            }
        }

        private void UpdateSelectedElement(ConfirmEventArgs args)
        {
            if (!args.IsConfirmed)
            {
                // Unconfirmed, abort
                return;
            }

            // Proceed in replacing the element
            FlexElement? newPrototype = Prototypes.FirstOrDefault(p => p.TypeCaption == _newElementName);
            if (newPrototype != null)
            {
                Element = newPrototype;
                ElementChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void RaiseDeleteClicked()
        {
            DeleteClicked?.Invoke(this, new EventArgs());
        }
    }
}
