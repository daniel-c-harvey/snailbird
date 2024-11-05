using Microsoft.AspNetCore.Components;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorCore.Confirmation;
using System.Reflection;

namespace RazorCore.Navigation
{
    public class Navigator<TMode, TModel> : INavigator<TMode>
        where TModel : IMode<TMode>
    {
        public TModel Model { get; }
        public PromptViewModel NavigateConfirmationViewModel { get; }

        public event ModeChangeEventHandler<TMode>? ModeChanged;
        public event EventHandler? ConfirmPrompt;
        public event ConfirmEventHandler? PromptBeforeNavigate;

        public TMode CurrentMode => Model.CurrentMode;
        public bool CanNavigateBack => modeHistory.Any();

        protected Stack<TMode> modeHistory = new Stack<TMode>();

        private static IEnumerable<NavigatePromptChoices> Choices =>
            [
                NavigatePromptChoices.Cancel,
                NavigatePromptChoices.Discard,
                NavigatePromptChoices.Save
            ];

        public Navigator(TModel model)
        {
            Model = model;
            NavigateConfirmationViewModel = new(Choices.Select(c => c.Choice));
        }

        private TMode? _nextMode;
        public INavigator<TMode> NavigateForward(TMode newMode)
        {
            _nextMode = newMode;
            if (NavigateConfirmationViewModel.IsConfigured && ConfirmPrompt != null && ShouldPrompt())
            {
                NavigateConfirmationViewModel.Choices[NavigatePromptChoices.Discard.Choice] = OnNavigateForward;
                ConfirmPrompt?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnNavigateForward();
            }
            return this;
        }

        private void OnNavigateForward()
        {
            if (_nextMode != null)
            {
                if (Model.CurrentMode != null)
                {
                    modeHistory.Push(Model.CurrentMode);
                }
                Model.CurrentMode = _nextMode;
                _nextMode = default;
                ResetConfirmationPrompt();
                OnChange();
            }
        }

        public INavigator<TMode> NavigateBack()
        {
            if (NavigateConfirmationViewModel.IsConfigured && ConfirmPrompt != null && ShouldPrompt())
            {
                NavigateConfirmationViewModel.Choices[NavigatePromptChoices.Discard.Choice] = OnNavigateBack;
                ConfirmPrompt?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnNavigateBack();
            }
            return this;
        }

        private void OnNavigateBack()
        {
            TMode? newMode;
            if (modeHistory.TryPop(out newMode))
            {
                Model.CurrentMode = newMode;
                ResetConfirmationPrompt();
                OnChange();
            }
        }

        private void ResetConfirmationPrompt()
        {
            NavigateConfirmationViewModel.Reset();
            PromptBeforeNavigate = null;
        }

        private bool ShouldPrompt()
        {
            ConfirmEventArgs shouldPrompt = new();
            PromptBeforeNavigate?.Invoke(this, shouldPrompt);
            return shouldPrompt.IsConfirmed;
        }

        public INavigator<TMode> ConfirmBeforeNavigateAway(PromptMessage model)
        {
            return ConfirmBeforeNavigateAway(model, null);
        }

        public INavigator<TMode> ConfirmBeforeNavigateAway(PromptMessage model, ConfirmEventHandler? confirmEventHandler)
        {
            NavigateConfirmationViewModel.PromptMessage = model;
            if (confirmEventHandler != null) PromptBeforeNavigate += confirmEventHandler;
            return this;
        }

        private static void Confirm(object? sender, ConfirmEventArgs args)
        {
            args.IsConfirmed = true;
        }

        protected void OnChange()
        {
            if (ModeChanged != null) 
            {
                ModeChanged(new ModeChangeEventArgs<TMode>(Model.CurrentMode));
            }
        }
    }
}
