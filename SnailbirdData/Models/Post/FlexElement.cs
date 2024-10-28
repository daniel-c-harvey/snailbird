using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models.Post
{
    public abstract class FlexElement
    {
        public int Ordinal { get; set; }
        public abstract string TypeCaption { get; }
    }

    public class FlexParagraph : FlexElement
    {
        public override string TypeCaption => "Paragraph";
        public string Text { get; set; }
    }

    public class FlexImage : FlexElement
    {
        public override string TypeCaption => "Image";
        public string ImageURI { get; set; }
        public string AltText { get; set; }
    }

    public class FlexYouTubeEmbed : FlexElement
    {
        public override string TypeCaption => "YouTube Embed";
        public string VideosURL { get; set; }
    }

    public class FlexInstrumentList : FlexElement
    {
        public override string TypeCaption => "Instrument List";
        public IList<Instrument> Instruments { get; set; }
    }
}
