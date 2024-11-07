using Core;
using Microsoft.AspNetCore.Components;
using NetBlocks;
using System.Text;

namespace RazorCore
{
    public partial class MarkupTextArea
    {
        private string _markdownText = default!;
        private string _processedText = default!;
        
        [Parameter]
        public required string Text 
        {
            get => _processedText;
            set
            {
                _markdownText = value;
                processMarkdown();
                StateHasChanged();
            }
        }

        public string RawText
        {
            get => _markdownText;
            set => Text = value;
        }

        private void processMarkdown()
        {
            StringBuilder processor = new(_markdownText);

            MarkupExpander expander = new MarkupExpander();
            ExpansionBuilder urlExpansion = expander.BeginNewExpansion();

            for (int index = 0; index < processor.Length; index++)
            {
                processHyperlinkAtIndex(processor, expander, ref urlExpansion, index);

                // process escape characters last
            }

            expander.Expand(processor);

            _processedText = processor.ToString();
        }

        private static void processHyperlinkAtIndex(StringBuilder processor, MarkupExpander expander, ref ExpansionBuilder expansion, int index)
        {
            // Mark elements for replacement
            if(expansion.OnDeck == null)
            {
                expansion.BeginNewSpan();
            }

            if (expansion.OnDeck != null)
            {
                if (expansion.OnDeck.Start == null)
                {
                    if (processor[index] == '[' &&
                       (index == 0 || processor[index - 1] != '\\'))
                    {
                        expansion.OnDeck.Start = index + 1;
                    }
                }
                else if (expansion.OnDeck.End == null && expansion.Spans.Count() < 2)
                {
                    if (processor[index] == ']' &&
                        (index > 0 && processor[index - 1] != '\\') &&
                        (index < processor.Length - 1 && processor[index + 1] == '('))
                    {
                        expansion.OnDeck.End = index;
                        expansion.OnDeck.Content = processor.Substring(expansion.OnDeck.Start.Value,
                                                                       expansion.OnDeck.End.Value);
                        expansion.BeginNewSpan();
                        expansion.OnDeck.Start = index + 2;
                    }
                }
                else if (expansion.OnDeck.Start != null && expansion.OnDeck.End == null)
                {
                    if (processor[index] == ')' &&
                         (index > 0 && processor[index - 1] != '\\'))
                    {
                        expansion.OnDeck.End = index;
                        expansion.OnDeck.Content = processor.Substring(expansion.OnDeck.Start.Value, 
                                                                        expansion.OnDeck.End.Value);
                    }
                }

                if (expansion.Spans.Count() == 2 && expansion.Spans.All(s => !string.IsNullOrWhiteSpace(s.Content)))
                {
                    expansion.Expansion = new($"[{expansion.Spans[0].Content}]({expansion.Spans[1].Content})",
                                              $"<a href=\"{expansion.Spans[0].Content}\">{expansion.Spans[1].Content}</a>");
                    expansion = expander.BeginNewExpansion();
                }
            }
        }
    }
}
