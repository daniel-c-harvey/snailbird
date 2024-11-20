using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdMedia
{
    public class SnailbirdImage
    {
        public MemoryStream ImageBinary { get; }
        public string Extension { get; }
        public int Size { get; }

        public SnailbirdImage(string ext, int size) 
        {
            ImageBinary = new MemoryStream();
            Extension = ext;
            Size = size;
        }

        public async Task LoadStreamAsync(Stream inStream)
        {
            int bufferSize = 81920;
            await inStream.CopyToAsync(ImageBinary, bufferSize);
        }
    }
}
