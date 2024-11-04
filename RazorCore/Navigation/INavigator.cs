using Core;
using Microsoft.AspNetCore.Components;
using RazorCore.Confirmation;

namespace RazorCore.Navigation
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
        event ConfirmEventHandler ConfirmNavigate;
        event ConfirmEventHandler PromptBeforeNavigate;
        PromptViewModel NavigateAwayConfirmationViewModel { get; }

        bool CanNavigateBack { get; }

        INavigator<TMode> NavigateForward(TMode newMode);
        INavigator<TMode> NavigateBack();
        INavigator<TMode> ConfirmBeforeNavigateAway(PromptModel model);
        INavigator<TMode> ConfirmBeforeNavigateAway(PromptModel model, ConfirmEventHandler promptCondition);
    }
}
