namespace RazorCore.Navigation
{


    public interface INavigable<TMode>
    {
        INavigator<TMode> Navigator { get; }
    }
}
