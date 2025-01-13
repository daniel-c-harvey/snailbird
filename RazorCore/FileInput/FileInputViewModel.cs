using Microsoft.AspNetCore.Components.Forms;
using NetBlocks.Models;

namespace RazorCore.FileInput;

public class FileInputViewModel
{
    public MediaContainer? File { get; set; }
    public event EventHandler? FileValidationFailed;
    public long MaximumSize { get; }

    public FileInputViewModel(long maximumSize)
    {
        MaximumSize = maximumSize;
    }
    public async Task<bool> OnFileChosen(IBrowserFile file)
    {
        if (!ValidateBrowserFile(file)) return false;
        
        try
        {
            const int CHUNK_SIZE = 8 * 1024;
            Stream stream = file.OpenReadStream(MaximumSize);

            int length = (int)stream.Length;
            byte[] bytes = new byte[length];
                
            for (int offset = 0; offset < length; offset += CHUNK_SIZE)
            {
                await stream.ReadAsync(bytes, offset, Math.Min(length - offset - 1, CHUNK_SIZE));
            }
                
            MIME.EXTENSIONS.TryGetValue(file.ContentType, out string? extension);
            if (length > 0 && extension != null)
            {
                File = new MediaContainer(file.Name, new MediaBinary(bytes, stream.Length, extension));
            }

            stream.Close();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex);
            return false;
        }

        return true;
    }
    
    private bool ValidateBrowserFile(IBrowserFile file)
    {
        // Test all validations and notify of failure
        if (!ValidateBrowserFileSize(file))
        {
            FileValidationFailed?.Invoke(this, EventArgs.Empty);
            return false;
        }

        return true;
    }

    private bool ValidateBrowserFileSize(IBrowserFile file)
    {
        if (file.Size <= MaximumSize) return true;
        
        // notify too large
        
        return false;
    }
}