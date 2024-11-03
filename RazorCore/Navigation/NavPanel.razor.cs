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

                Context.Navigator.ModeChanged += OnModeChange;
            }
        }

        protected void OnModeChange(ModeChangeEventArgs<TMode> args)
        {
            StateHasChanged();
        }

        protected void OnNavigateBack(MouseEventArgs e)
        {
            
            if (Context != null)
            {
                Context.Navigator.NavigateBack();
                StateHasChanged();
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
