using Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace RazorCore.Navigation
{
    public partial class NavPanel<TMode>
    {
        [Parameter]
        public RenderFragment<INavigable<TMode>>? ChildContent { get; set; }

        [Parameter]
        public required INavigable<TMode> ViewModel { get; set; }

        [Parameter]
        public EventCallback<ModeChangeEventArgs<TMode>> OnModeChanged { get; set; }

        private Confirmation.Confirmation confirmation;

        protected override void OnParametersSet()
        {
            InitNavigation();
            ViewModel.Navigator.ModeChanged += OnModeChange;
            ViewModel.Navigator.ConfirmNavigate += OpenConfirmation;
            base.OnParametersSet();
        }

        protected void OnModeChange(ModeChangeEventArgs<TMode> args)
        {
            StateHasChanged();
        }

        protected void OnNavigateBack(MouseEventArgs e)
        {
            
            if (ViewModel != null)
            {
                ViewModel.Navigator.NavigateBack();
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

        private void OpenConfirmation(object sender, ConfirmEventArgs e)
        {
            if (confirmation != null)
            {
                confirmation.Open();
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
