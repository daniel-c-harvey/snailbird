
using NetBlocks;

namespace SnailbirdData.Models.Post
{
    public abstract class FlexElement : ICloneable<FlexElement>
    {
        //public int Ordinal { get; set; }
        public abstract string TypeCaption { get; }

        public abstract FlexElement Clone();
    }

    public class FlexParagraph : FlexElement
    {
        public override string TypeCaption => "Paragraph";
        public string Text { get; set; } = string.Empty;

        public override FlexElement Clone()
        {
            return new FlexParagraph() { Text = Text };
        }
    }

    public class FlexImage : FlexElement
    {
        public override string TypeCaption => "Image";
        public string ImageURI { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;

        public override FlexElement Clone()
        {
            return new FlexImage() { ImageURI = ImageURI, 
                                     AltText = AltText };
        }
    }

    public class FlexYouTubeEmbed : FlexElement
    {
        public override string TypeCaption => "YouTube Embed";
        public string VideosURL { get; set; } = string.Empty;

        public override FlexElement Clone()
        {
            return new FlexYouTubeEmbed() { VideosURL = VideosURL };
        }
    }

    public class FlexInstrumentList : FlexElement
    {
        public override string TypeCaption => "Instrument List";
        public IList<Instrument> Instruments { get; set; } = [];

        public override FlexElement Clone()
        {
            return new FlexInstrumentList() { Instruments = Instruments };
        }
    }
}
