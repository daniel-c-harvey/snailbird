using Microsoft.AspNetCore.Components.Forms;
using NetBlocks.Models;
using NetBlocks.Models.FileBinary;

namespace RazorCore.FileInput;

public class ImageFileInputViewModel : FileInputViewModel<ImageBinary, ImageBinaryDto, ImageBinaryParams>
{
    public ImageFileInputViewModel(long maximumSize) : base(maximumSize) { }

    protected override ImageBinaryParams CreateParams(IBrowserFile file, byte[] bytes)
    {
        MIME.Extensions.TryGetValue(file.ContentType, out string? extension);
        return new ImageBinaryParams
        {
            Data = bytes,
            Size = bytes.Length,
            Extension = extension ?? throw new Exception("The image extension is missing."),
            AspectRatio = 1D // TODO get a real value
        };
    }
}