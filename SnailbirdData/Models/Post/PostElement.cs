using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models.Post
{
    public class PostElement
    {
        public int Ordinal { get; set; }
    }
    
    public class PostParagraph : PostElement
    {
        public string Text { get; set; }
    }

    public class PostImage : PostElement
    {
        public string ImageURI { get; set; }
        public string AltText { get; set; }
    }

    public class PostYouTubeEmbed : PostElement
    {
        public string VideosURL { get; set; }
    }

    public class PostInstrumentList : PostElement
    {
        public IEnumerable<Instrument> Instruments { get; set; }
    }
}
