using Microsoft.AspNetCore.Components;
using NetBlocks.Models;
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
        PromptViewModel NavigateConfirmationViewModel { get; }
        bool CanNavigateBack { get; }

        event ModeChangeEventHandler<TMode> ModeChanged;
        event EventHandler ConfirmPrompt;
        event ConfirmEventHandler PromptBeforeNavigate;

        INavigator<TMode> NavigateForward(TMode newMode);
        INavigator<TMode> NavigateBack();
        INavigator<TMode> ConfirmBeforeNavigateAway(PromptMessage model);
        INavigator<TMode> ConfirmBeforeNavigateAway(PromptMessage model, ConfirmEventHandler promptCondition);
    }
}
