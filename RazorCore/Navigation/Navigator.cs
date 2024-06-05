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

        public event ModeChangeEventHandler<TMode>? ModeAdvancing;
        public event ModeChangeEventHandler<TMode> ModeChanged;

        public Navigator(TModel model)
        {
            Model = model;
        }

        public void OnForward()
        {
            if (ModeAdvancing != null)
            {
                ModeAdvancing(new ModeChangeEventArgs<TMode>(Model.CurrentMode));
                OnChange();
            }
        }

        public void OnBack(TMode mode)
        {
            Model.CurrentMode = mode;
            OnChange();
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
