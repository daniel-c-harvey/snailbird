using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Driver;
using NetBlocks.Models;
using NetBlocks.Models.Environment;
using RazorCore.Confirmation;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdData.Models.Post;
using SnailbirdMedia.Clients;
using SnailbirdMedia.Configs;
using SnailbirdMedia.Models;

namespace SnailbirdAdminWeb.Client.ViewModels.EditFlex.Element
{
    public class EditFlexImageViewModel
    {
        [Inject]
        public IVaultManagerClient? Vault { get; protected set; }

        private const int MAXIMUM_FILE_SIZE = 2 * 1024 * 1024; // 2MB

        public event EventHandler ImageSizeExceeded;
        public FlexImage FlexImage { get; protected set; }
        protected MediaBinary? Image { get; set; }
        public string DataURL { get; protected set; } = default!;



        public EditFlexImageViewModel(FlexImage flexImage)
        {
            FlexImage = flexImage;
            OnImageLoaded().Wait();
        }

        private async Task OnImageLoaded()
        {
            if (!string.IsNullOrWhiteSpace(FlexImage.ImageURI))
            {
                Image = await Vault.GetMedia(FlexImage.ImageURI);
            }
            SetDataURL();
        }

        public void SetImageURI(string? value)
        {
            if (value != null)
            {
                FlexImage.ImageURI = value;
            }
        }
            

        public async Task OnImageChanged(IBrowserFile file)
        {
            if (file.Size > MAXIMUM_FILE_SIZE)
            {
                // notify too large
                ImageSizeExceeded?.Invoke(this, EventArgs.Empty);
                return;
            }
            try
            {
                const int CHUNK_SIZE = 64;// 8 * 1024;
                Stream stream = file.OpenReadStream(MAXIMUM_FILE_SIZE);

                int length = (int)stream.Length;
                byte[] bytes = new byte[length];
                

                for (int offset = 0; offset < length; offset += CHUNK_SIZE)
                {
                    await stream.ReadAsync(bytes, offset, Math.Min(length - offset - 1, CHUNK_SIZE));
                }


                string? extension = null;
                MIME.EXTENSIONS.TryGetValue(file.ContentType, out extension);
            
                if (length > 0 && extension != null)
                {
                    // TESTING
                    var file2 = File.OpenWrite($"test{extension}");
                    await file2.WriteAsync(bytes);
                    await file2.FlushAsync();
                    file2.Close();
                    // /TESTING

                    //Image = new(buffer.ToArray(), stream.Length, extension);
                    Image = new(bytes, stream.Length, extension);
                    SetDataURL(); // LKESS THA N HALF MEG DATA URL EVEYONE CRAZY GOTTA MAKE THE FILE SIZE WORK
                }

                stream.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }

        private void SetDataURL()
        {
            string? mime;
            string base64;
            if (Image != null)
            {
                base64 = Image.Base64;
                MIME.MIME_TYPES.TryGetValue(Image.Extension, out mime);
            }
            else
            {
                base64 = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48IS0tIFVwbG9hZGVkIHRvOiBTVkcgUmVwbywgd3d3LnN2Z3JlcG8uY29tLCBHZW5lcmF0b3I6IFNWRyBSZXBvIE1peGVyIFRvb2xzIC0tPgo8c3ZnIGZpbGw9IiMwMDAwMDAiIHdpZHRoPSI4MDBweCIgaGVpZ2h0PSI4MDBweCIgdmlld0JveD0iMCAwIDMyIDMyIiBpZD0iaWNvbiIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZGVmcz48c3R5bGU+LmNscy0xe2ZpbGw6bm9uZTt9PC9zdHlsZT48L2RlZnM+PHRpdGxlPm5vLWltYWdlPC90aXRsZT48cGF0aCBkPSJNMzAsMy40MTQxLDI4LjU4NTksMiwyLDI4LjU4NTksMy40MTQxLDMwbDItMkgyNmEyLjAwMjcsMi4wMDI3LDAsMCwwLDItMlY1LjQxNDFaTTI2LDI2SDcuNDE0MWw3Ljc5MjktNy43OTMsMi4zNzg4LDIuMzc4N2EyLDIsMCwwLDAsMi44Mjg0LDBMMjIsMTlsNCwzLjk5NzNabTAtNS44MzE4LTIuNTg1OC0yLjU4NTlhMiwyLDAsMCwwLTIuODI4NCwwTDE5LDE5LjE2ODJsLTIuMzc3LTIuMzc3MUwyNiw3LjQxNDFaIi8+PHBhdGggZD0iTTYsMjJWMTlsNS00Ljk5NjYsMS4zNzMzLDEuMzczMywxLjQxNTktMS40MTYtMS4zNzUtMS4zNzVhMiwyLDAsMCwwLTIuODI4NCwwTDYsMTYuMTcxNlY2SDIyVjRINkEyLjAwMiwyLjAwMiwwLDAsMCw0LDZWMjJaIi8+PHJlY3QgaWQ9Il9UcmFuc3BhcmVudF9SZWN0YW5nbGVfIiBkYXRhLW5hbWU9IiZsdDtUcmFuc3BhcmVudCBSZWN0YW5nbGUmZ3Q7IiBjbGFzcz0iY2xzLTEiIHdpZHRoPSIzMiIgaGVpZ2h0PSIzMiIvPjwvc3ZnPg==";
                mime = "image/svg+xml";
            }
            DataURL = $"data:{mime};base64,{base64}";
            //ImageChanged?.Invoke(this, EventArgs.Empty);
        }


    }
}
