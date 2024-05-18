using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace RazorCore.Navigation
{
    public partial class NavPanel<TMode>
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public Func<INavigable<TMode>>? GetContext { get; set; }

        private INavigable<TMode>? Context;
        protected Stack<TMode> modeHistory = new Stack<TMode>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            InitNavigation();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                if (GetContext is null) throw new ArgumentNullException(nameof(GetContext));

                Context = GetContext();

                if (Context is null) throw new ArgumentNullException(nameof(Context));

                Context.Navigator.ModeChanging += OnModeChange;
            }
        }

        protected void OnModeChange(ModeChangeEventArgs<TMode> args)
        {
            modeHistory.Push(args.oldMode);
        }

        protected void OnNavigateBack(MouseEventArgs e)
        {
            TMode? newMode;
            if (Context is not null && modeHistory.TryPop(out newMode))
            {
                Context.Navigator.OnBack(newMode);
            }
        }

        [Inject]
        NavigationManager? NavigationManager { get; set; }
        private void InitNavigation()
        {
            if (NavigationManager != null)
            {
                NavigationManager.LocationChanged += HandleLocationChange;
            }
        }


        protected void HandleLocationChange(object? sender, LocationChangedEventArgs e)
        {
            if (!e.IsNavigationIntercepted)
            {
                // todo interrupt naviagting away from a dirty page
                //StateHasChanged();
            }
        }
    }
}
