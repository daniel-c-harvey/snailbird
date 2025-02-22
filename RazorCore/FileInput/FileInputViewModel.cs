﻿using Microsoft.AspNetCore.Components.Forms;
using NetBlocks.Models;
using NetBlocks.Models.FileBinary;
using NetBlocks.Models.FileBinary.Factory;

namespace RazorCore.FileInput;

public abstract class FileInputViewModel<TMedia, TDto, TParams>
where TMedia : MediaBinary<TMedia, TDto, TParams>, new()
where TDto : MediaBinaryDto<TMedia, TDto, TParams>
where TParams : MediaBinaryParams
{
    public MediaContainer<TMedia, TDto, TParams>? File { get; private set; }
    public bool Expanded { get; set; }
    public event MessageEventHandler? FileValidationFailed;
    private long _maximumSize;
    

    public FileInputViewModel(long maximumSize)
    {
        _maximumSize = maximumSize;
    }
    
    public async Task<bool> OnFileChosen(IBrowserFile file)
    {
        if (!ValidateBrowserFile(file)) return false;
        
        try
        {
            const int chunkSize = 8 * 1024;
            Stream stream = file.OpenReadStream(_maximumSize);

            int length = (int)stream.Length;
            byte[] bytes = new byte[length];
                
            for (int offset = 0; offset < length; offset += chunkSize)
            {
                await stream.ReadAsync(bytes, offset, Math.Min(length - offset - 1, chunkSize));
            }
                
            TParams parameters = CreateParams(file, bytes);
            
            if (parameters.Size > 0 && !string.IsNullOrWhiteSpace(parameters.Extension))
            {
                File = new MediaContainer<TMedia, TDto, TParams>(file.Name, MediaFactory.CreateFromParams<TMedia, TDto, TParams>(parameters));
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
    
    protected abstract TParams CreateParams(IBrowserFile file, byte[] bytes);

    private bool ValidateBrowserFile(IBrowserFile file)
    {
        // Test all validations and notify of failure
        if (!ValidateBrowserFileSize(file))
        {
            return false;
        }

        // Add other validations here
        
        return true;
    }

    private bool ValidateBrowserFileSize(IBrowserFile file)
    {
        if (file.Size <= _maximumSize) return true;
        
        FileValidationFailed?.Invoke(this, new MessageEventArgs($"The image exceeds the maximum allowed file size: {_maximumSize / (1024D * 1024D)} MB"));
        
        return false;
    }
    
    public void Reset()
    {
        Expanded = false;
        File = null;
    }
}