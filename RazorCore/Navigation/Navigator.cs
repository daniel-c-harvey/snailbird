using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore.Navigation
{
    public class Navigator<TMode, TModel> : INavigator<TMode>
        where TModel : IMode<TMode>
    {
        public TModel Model { get; }

        public TMode CurrentMode => Model.CurrentMode;

        public event ModeChangeEventHandler<TMode>? ModeChanged;

        public bool CanNavigateBack => modeHistory.Any();

        protected Stack<TMode> modeHistory = new Stack<TMode>();

        public Navigator(TModel model)
        {
            Model = model;
        }

        public void NavigateForward(TMode newMode)
        {
            if (Model.CurrentMode != null)
            {
                modeHistory.Push(Model.CurrentMode);
            }
            Model.CurrentMode = newMode;
            OnChange();
        }

        public void NavigateBack()
        {
            TMode? newMode;
            if (modeHistory.TryPop(out newMode))
            {
                Model.CurrentMode = newMode;
                OnChange();
            }
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
