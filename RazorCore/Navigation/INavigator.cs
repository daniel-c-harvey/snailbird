﻿namespace RazorCore.Navigation
{
    public class ModeChangeEventArgs<TMode> : EventArgs
    {
        public TMode oldMode;

        public ModeChangeEventArgs(TMode oldMode)
        {
            this.oldMode = oldMode;
        }
    }

    public delegate void ModeChangeEventHandler<TMode>(ModeChangeEventArgs<TMode> args);

    public interface INavigator<TMode>
    {
        TMode CurrentMode { get; }

        event ModeChangeEventHandler<TMode> ModeChanged;

        bool CanNavigateBack { get; }

        void NavigateForward(TMode newMode);
        void NavigateBack();
    }
}
