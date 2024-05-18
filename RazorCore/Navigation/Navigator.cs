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

        public event ModeChangeEventHandler<TMode>? ModeChanging;


        public Navigator(TModel model)
        {
            Model = model;
        }

        public void OnForward()
        {
            if (ModeChanging != null)
            {
                ModeChanging(new ModeChangeEventArgs<TMode>(Model.CurrentMode));
            }
        }

        public void OnBack(TMode mode)
        {
            Model.CurrentMode = mode;
        }
    }
}
