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
        public PromptViewModel NavigateAwayConfirmationViewModel { get; }

        public event ModeChangeEventHandler<TMode>? ModeChanged;
        public event ConfirmEventHandler? ConfirmNavigate;
        public event ConfirmEventHandler? PromptBeforeNavigate;

        public TMode CurrentMode => Model.CurrentMode;
        public bool CanNavigateBack => modeHistory.Any();

        protected Stack<TMode> modeHistory = new Stack<TMode>();

        public Navigator(TModel model)
        {
            Model = model;
            NavigateAwayConfirmationViewModel = new();
        }

        private TMode? _nextMode;
        public INavigator<TMode> NavigateForward(TMode newMode)
        {
            _nextMode = newMode;
            if (NavigateAwayConfirmationViewModel.IsConfigured && ConfirmNavigate != null && ShouldPrompt())
            {
                NavigateAwayConfirmationViewModel.OnClose = OnNavigateForward;
                ConfirmNavigate?.Invoke(this, new ConfirmEventArgs());
            }
            else
            {
                OnNavigateForward(new ConfirmEventArgs() { IsConfirmed = true });
            }
            return this;
        }

        private void OnNavigateForward(ConfirmEventArgs args)
        {
            if (args.IsConfirmed && _nextMode != null)
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
            if (NavigateAwayConfirmationViewModel.IsConfigured && ConfirmNavigate != null && ShouldPrompt())
            {
                NavigateAwayConfirmationViewModel.OnClose = OnNavigateBack;
                ConfirmNavigate?.Invoke(this, new ConfirmEventArgs());
            }
            else
            {
                OnNavigateBack(new ConfirmEventArgs() { IsConfirmed = true });
            }
            return this;
        }

        private void OnNavigateBack(ConfirmEventArgs args)
        {
            if (args.IsConfirmed)
            {
                TMode? newMode;
                if (modeHistory.TryPop(out newMode))
                {
                    Model.CurrentMode = newMode;
                    ResetConfirmationPrompt();
                    OnChange();
                }
            }
        }

        private void ResetConfirmationPrompt()
        {
            NavigateAwayConfirmationViewModel.Reset();
            PromptBeforeNavigate = null;
        }

        private bool ShouldPrompt()
        {
            ConfirmEventArgs shouldPrompt = new();
            PromptBeforeNavigate?.Invoke(this, shouldPrompt);
            return shouldPrompt.IsConfirmed;
        }

        public INavigator<TMode> ConfirmBeforeNavigateAway(PromptModel model)
        {
            return ConfirmBeforeNavigateAway(model, null);
        }

        public INavigator<TMode> ConfirmBeforeNavigateAway(PromptModel model, ConfirmEventHandler? confirmEventHandler)
        {
            NavigateAwayConfirmationViewModel.Prompt = model;
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
