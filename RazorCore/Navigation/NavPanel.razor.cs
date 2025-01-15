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

        private Confirmation.Prompt confirmation;

        protected override void OnParametersSet()
        {
            if (ViewModel is null) return;
            
            InitNavigation();
            ViewModel.Navigator.ModeChanged += OnModeChange;
            ViewModel.Navigator.ConfirmPrompt += OpenConfirmation;
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

        private void OpenConfirmation(object? sender, EventArgs e)
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
