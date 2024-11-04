
using NetBlocks;

namespace SnailbirdData.Models.Post
{
    public abstract class FlexElement : ICloneable<FlexElement>
    {
        public abstract string TypeCaption { get; }

        public abstract FlexElement Clone();

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            var other = obj as FlexElement;
            return other != null && TypeCaption.Equals(other.TypeCaption);
        }

        public override int GetHashCode()
        {
            return TypeCaption.GetHashCode();
        }
    }

    public class FlexParagraph : FlexElement
    {
        public override string TypeCaption => "Paragraph";
        public string Text { get; set; } = string.Empty;

        public override FlexElement Clone()
        {
            return new FlexParagraph() { Text = Text };
        }

        public override bool Equals(object? obj)
        {
            var other = obj as FlexParagraph;
            return base.Equals(other) && Text.Equals(other.Text);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Text.GetHashCode();
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

        public override bool Equals(object? obj)
        {
            var other = obj as FlexImage;
            return base.Equals(other) && 
                   ImageURI.Equals(other.ImageURI) && 
                   AltText.Equals(other.AltText);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ 
                   ImageURI.GetHashCode() ^ 
                   AltText.GetHashCode();
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

        public override bool Equals(object? obj)
        {
            var other = obj as FlexYouTubeEmbed;
            return base.Equals(other) && 
                   VideosURL.Equals(other.VideosURL);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ VideosURL.GetHashCode();
        }
    }

    public class FlexInstrumentList : FlexElement
    {
        public override string TypeCaption => "Instrument List";
        public IList<Instrument> Instruments { get; set; } = [];

        public override FlexElement Clone()
        {
            return new FlexInstrumentList() { Instruments = Instruments.Select(i => i.Clone()).ToList() };
        }

        public override bool Equals(object? obj)
        {
            var other = obj as FlexInstrumentList;
            return base.Equals(other) &&
                   Instruments.SequenceEqual(other.Instruments);
        }

        public override int GetHashCode()
        {
            return Instruments.Aggregate(base.GetHashCode(), (sofar, next) => sofar ^ next.GetHashCode());
        }
    }
}
