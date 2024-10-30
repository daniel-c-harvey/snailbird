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
        private static Dictionary<string, Func<FlexElement>> ElementChoiceMap = new Dictionary<string, Func<FlexElement>>
        {
            { "Paragraph",  () => new FlexParagraph() },
            { "Image", () => new FlexImage() },
            { "YouTube Embed", () => new FlexYouTubeEmbed() },
            { "Instrument List", () => new FlexInstrumentList() }
        };

        public static IEnumerable<string> ElementChoices => ElementChoiceMap.Keys;

        private FlexElement _element;
        public FlexElement Element => _element;

        public EditFlexElementViewModel(FlexElement element)
        {
            _element = element;
        }

        public void ReplaceElement(string elementName)
        {
            Func<FlexElement>? build = null;
            if (ElementChoiceMap.TryGetValue(elementName, out build))
            {
                _element = build();
            }
        }
    }
}
