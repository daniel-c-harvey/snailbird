using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore.CanvasImage
{
    public interface ICanvasImageService : IAsyncDisposable
    {
        Task InitializeAsync(ElementReference canvasRef);
        Task DrawImageAsync(string base64Image);
    }

    public class CanvasImageService : ICanvasImageService
    {
        private readonly IJSRuntime _js;
        private IJSObjectReference? _module;
        private ElementReference _canvasReference;

        public CanvasImageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task InitializeAsync(ElementReference canvasRef)
        {
            _canvasReference = canvasRef;
            _module = await _js.InvokeAsync<IJSObjectReference>(
                "import", "./_content/RazorCore/js/canvasImage.js");
        }

        public async Task DrawImageAsync(string base64Image)
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Canvas not initialized");
            }
            await _module.InvokeVoidAsync("drawImage", _canvasReference, base64Image);
        }

        public async ValueTask DisposeAsync()
        {
            if (_module is not null)
            {
                await _module.DisposeAsync();
            }
        }
    }
}
