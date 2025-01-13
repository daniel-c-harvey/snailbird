using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NetBlocks.Models;

namespace RazorCore.FileInput;

public partial class FileInput
{
    [Parameter]
    public required FileInputViewModel ViewModel { get; set; }
    [Parameter]
    public EventCallback<MediaContainer> OnFileSelected { get; set; }
    
    private async Task OnFileChanged(InputFileChangeEventArgs e)
    {
        if (e.FileCount == 1) // only one file for now
        {
            try
            {
                if (!await ViewModel.OnFileChosen(e.File)) return;

                await OnFileSelected.InvokeAsync(ViewModel.File);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        StateHasChanged();
    }
}