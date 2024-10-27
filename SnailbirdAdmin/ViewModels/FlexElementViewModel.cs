using SnailbirdData.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdAdmin.ViewModels
{
    public class FlexElementViewModel
    {
        public static Dictionary<string, Type> ElementChoiceMap = new Dictionary<string, Type>
        {
            { "Paragraph", typeof(FlexParagraph) },
            { "Image", typeof(FlexImage) },
            { "YouTube Embed", typeof(PostYouTubeEmbed) },
            { "Instrument List", typeof(PostInstrumentList) }
        };

        public FlexElement? Element { get; set; }
    }
}
