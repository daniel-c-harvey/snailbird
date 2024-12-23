using NetBlocks.Interfaces;
using NetBlocks.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SnailbirdData.Models.Post
{
    [JsonDerivedType(typeof(FlexParagraph), "paragraph")]
    [JsonDerivedType(typeof(FlexImage), "image")]
    [JsonDerivedType(typeof(FlexYouTubeEmbed), "youtube")]
    [JsonDerivedType(typeof(FlexInstrumentList), "instr")]
    public abstract class FlexElement : ICloneable<FlexElement>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            if (name != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

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
            bool q = other != null;
            q &= TypeCaption.Equals(other.TypeCaption);
            return q;
        }

        public override int GetHashCode()
        {
            return TypeCaption.GetHashCode();
        }
    }

    public class FlexParagraph : FlexElement
    {
        private string text = string.Empty;

        public override string TypeCaption => "Paragraph";
        public string Text
        {
            get => text; 
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged();
                }
            }
        }

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
