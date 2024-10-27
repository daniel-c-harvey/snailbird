using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models.Post
{

    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(FlexParagraph), 
                    typeof(FlexImage), 
                    typeof(PostYouTubeEmbed),
                    typeof(PostInstrumentList))]
    public abstract class FlexElement
    {
        public static IDictionary<string, string> PostElementTypeCaptions =
            new Dictionary<string, string>
            {
                { nameof(FlexParagraph), "Paragraph"},
                { nameof(FlexImage), "Image" },
                { nameof(PostYouTubeEmbed), "YouTube Embed" },
                { nameof(PostInstrumentList), "Instrument List" }
            };

        public int Ordinal { get; set; }
    }
    
    public class FlexParagraph : FlexElement
    {
        public string Text { get; set; }
    }

    public class FlexImage : FlexElement
    {
        public string ImageURI { get; set; }
        public string AltText { get; set; }
    }

    public class PostYouTubeEmbed : FlexElement
    {
        public string VideosURL { get; set; }
    }

    public class PostInstrumentList : FlexElement
    {
        public IList<Instrument> Instruments { get; set; }
    }
}
