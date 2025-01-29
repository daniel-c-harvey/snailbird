using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NetBlocks.Models.FileBinary;

namespace RazorCore.FileInput;

public abstract partial class FileInput<TMedia, TDto, TParams, TViewModel>
where TMedia : MediaBinary<TMedia, TDto, TParams>, new()
where TDto : MediaBinaryDto<TMedia, TDto, TParams>
where TParams : MediaBinaryParams
where TViewModel : FileInputViewModel<TMedia, TDto, TParams>
{
    [Parameter]
    public required TViewModel ViewModel { get; set; }
    [Parameter]
    public EventCallback<MediaContainer<TMedia, TDto, TParams>> OnFileSelected { get; set; }
    [Parameter]
    public bool ShowFileUri { get; set; } = false;
    
    protected string FileExpanderCss => (ViewModel.Expanded && ViewModel.File != null) ? "expanded" : "collapsed";

    protected void Expand()
    {
        if (ViewModel.File == null) return;
        
        ViewModel.Expanded = !ViewModel.Expanded;
        StateHasChanged();
    }
    protected async Task OnFileChanged(InputFileChangeEventArgs e)
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