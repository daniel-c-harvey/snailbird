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
        private static Dictionary<string, Type> ElementChoiceMap = new Dictionary<string, Type>
        {
            { nameof(PostParagraph), typeof(PostParagraph) },
            { nameof(PostImage), typeof(PostImage) },
            { nameof(PostYouTubeEmbed), typeof(PostYouTubeEmbed) },
            { nameof(PostInstrumentList), typeof(PostInstrumentList) }
        };

        public PostElement? Element { get; set; }
    }
}
