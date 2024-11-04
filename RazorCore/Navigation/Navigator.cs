using Microsoft.AspNetCore.Components;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorCore.Confirmation;

namespace RazorCore.Navigation
{
    public class Navigator<TMode, TModel> : INavigator<TMode>
        where TModel : IMode<TMode>
    {
        public TModel Model { get; }
        public PromptViewModel NavigateAwayConfirmationViewModel { get; }

        public event ModeChangeEventHandler<TMode>? ModeChanged;
        public event ConfirmEventHandler? ConfirmNavigate;

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
            if (NavigateAwayConfirmationViewModel.IsConfigured && ConfirmNavigate != null)
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
                NavigateAwayConfirmationViewModel.Reset();
                OnChange();
            }
        }

        public INavigator<TMode> NavigateBack()
        {
            if (NavigateAwayConfirmationViewModel.IsConfigured && ConfirmNavigate != null)
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
                    NavigateAwayConfirmationViewModel.Reset();
                    OnChange();
                }
            }
        }

        public INavigator<TMode> ConfirmBeforeNavigateAway(PromptModel model)
        {
            NavigateAwayConfirmationViewModel.Model = model;
            return this;
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
