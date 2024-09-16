using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models.Post
{

    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(PostParagraph), 
                    typeof(PostImage), 
                    typeof(PostYouTubeEmbed),
                    typeof(PostInstrumentList))]
    public abstract class PostElement
    {
        public static IDictionary<string, string> PostElementTypeCaptions =
            new Dictionary<string, string>
            {
                { nameof(PostParagraph), "Paragraph"},
                { nameof(PostImage), "Image" },
                { nameof(PostYouTubeEmbed), "YouTube Embed" },
                { nameof(PostInstrumentList), "Instrument List" }
            };

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
        public IList<Instrument> Instruments { get; set; }
    }
}
