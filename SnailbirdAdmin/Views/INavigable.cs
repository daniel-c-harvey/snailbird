using Microsoft.AspNetCore.Components;

namespace SnailbirdAdmin
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

    public interface INavigable<TMode>
    {
        TMode CurrentMode { get; }

        event ModeChangeEventHandler<TMode> ModeChanging;
        void OnBack(TMode mode);
    }
}
