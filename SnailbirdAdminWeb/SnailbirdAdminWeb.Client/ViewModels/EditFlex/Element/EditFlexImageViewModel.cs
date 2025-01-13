﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NetBlocks.Models;
using SnailbirdData.Models.Post;
using SnailbirdMedia.Clients;
using SnailbirdMedia.Models;

namespace SnailbirdAdminWeb.Client.ViewModels.EditFlex.Element
{
    public class EditFlexImageViewModel
    {
        private const int MAXIMUM_FILE_SIZE = 2 * 1024 * 1024; // 2MB
        private const string IMAGE_UNAVAILABLE_BASE64 = @"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48IS0tIFVwbG9hZGVkIHRvOiBTVkcgUmVwbywgd3d3LnN2Z3JlcG8uY29tLCBHZW5lcmF0b3I6IFNWRyBSZXBvIE1peGVyIFRvb2xzIC0tPgo8c3ZnIGZpbGw9IiMwMDAwMDAiIHdpZHRoPSI4MDBweCIgaGVpZ2h0PSI4MDBweCIgdmlld0JveD0iMCAwIDMyIDMyIiBpZD0iaWNvbiIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZGVmcz48c3R5bGU+LmNscy0xe2ZpbGw6bm9uZTt9PC9zdHlsZT48L2RlZnM+PHRpdGxlPm5vLWltYWdlPC90aXRsZT48cGF0aCBkPSJNMzAsMy40MTQxLDI4LjU4NTksMiwyLDI4LjU4NTksMy40MTQxLDMwbDItMkgyNmEyLjAwMjcsMi4wMDI3LDAsMCwwLDItMlY1LjQxNDFaTTI2LDI2SDcuNDE0MWw3Ljc5MjktNy43OTMsMi4zNzg4LDIuMzc4N2EyLDIsMCwwLDAsMi44Mjg0LDBMMjIsMTlsNCwzLjk5NzNabTAtNS44MzE4LTIuNTg1OC0yLjU4NTlhMiwyLDAsMCwwLTIuODI4NCwwTDE5LDE5LjE2ODJsLTIuMzc3LTIuMzc3MUwyNiw3LjQxNDFaIi8+PHBhdGggZD0iTTYsMjJWMTlsNS00Ljk5NjYsMS4zNzMzLDEuMzczMywxLjQxNTktMS40MTYtMS4zNzUtMS4zNzVhMiwyLDAsMCwwLTIuODI4NCwwTDYsMTYuMTcxNlY2SDIyVjRINkEyLjAwMiwyLjAwMiwwLDAsMCw0LDZWMjJaIi8+PHJlY3QgaWQ9Il9UcmFuc3BhcmVudF9SZWN0YW5nbGVfIiBkYXRhLW5hbWU9IiZsdDtUcmFuc3BhcmVudCBSZWN0YW5nbGUmZ3Q7IiBjbGFzcz0iY2xzLTEiIHdpZHRoPSIzMiIgaGVpZ2h0PSIzMiIvPjwvc3ZnPg==";

        public event EventHandler? ImageValidationFailed;

        [Inject]
        public IVaultManagerClient? Vault { get; protected set; }

        private MediaContainer? Image { get; set; }
        public FlexImage FlexImage { get; protected set; }
        public string DataURL { get; protected set; } = default!;
        public string Base64 => Image?.Binary.Base64 ?? IMAGE_UNAVAILABLE_BASE64;

        public EditFlexImageViewModel(FlexImage flexImage)
        {
            FlexImage = flexImage;
            OnImageLoaded().Wait();
        }

        public void OnImageSelected(MediaContainer image)
        {
            Image = image;
            SetDataURL();
        }

        private async Task OnImageLoaded()
        {
            if (!string.IsNullOrWhiteSpace(FlexImage.ImageURI))
            {
                // todo fix this
                //Image = await Vault.GetMedia(FlexImage.ImageURI);
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

        private void SetDataURL()
        {
            string? mime;
            string base64;
            if (Image != null)
            {
                base64 = Image.Binary.Base64;
                MIME.MIME_TYPES.TryGetValue(Image.Binary.Extension, out mime);
            }
            else
            {
                base64 = IMAGE_UNAVAILABLE_BASE64;
                mime = "image/svg+xml";
            }
            DataURL = $"data:{mime};base64,{base64}";
        }


    }
}
