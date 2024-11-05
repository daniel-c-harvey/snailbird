using Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore.Confirmation
{
    public partial class Prompt
    {
        [Parameter]
        public required PromptViewModel ViewModel { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        public void Open()
        {
            IsVisible = true;
            StateHasChanged();
        }

        private void Close(PromptChoice choice)
        {
            IsVisible = false;
            StateHasChanged();
            ViewModel.OnClose?.Invoke(new ResultEventArgs<PromptChoice>(choice));
        }
    }
}
