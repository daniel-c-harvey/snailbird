using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdMedia.Models
{
    public class MediaBinary
    {
        public string Base64 { get; set; } = default!;
        public int Size { get; set; } = default!;
        public string Extension { get; set; } = default!;

        internal MediaBinary(MediaBinaryDto other) 
        {
            Base64 = Convert.ToBase64String(other.Bytes); // probably smart to make the API operate in base64 instead of byte arrays, it's much more compressed
            Size = other.Size;
            Extension = other.Extension;
        }
    }
}
