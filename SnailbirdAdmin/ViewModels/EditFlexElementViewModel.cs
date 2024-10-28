using SnailbirdData.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdAdmin.ViewModels
{
    public class EditFlexElementViewModel
    {
        public static Dictionary<string, Type> ElementChoiceMap = new Dictionary<string, Type>
        {
            { "Paragraph", typeof(FlexParagraph) },
            { "Image", typeof(FlexImage) },
            { "YouTube Embed", typeof(FlexYouTubeEmbed) },
            { "Instrument List", typeof(FlexInstrumentList) }
        };

        public FlexElement Element { get; set; }
    }
}
