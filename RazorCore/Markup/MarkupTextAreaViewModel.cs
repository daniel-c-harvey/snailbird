using NetBlocks.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RazorCore.Markup
{
    public class MarkupTextAreaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            if (name != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }


        private string _rawText = default!;

        public MarkupParagraphViewModel ParagraphViewModel { get; set; }

        public MarkupTextAreaViewModel(string markup)
        {
            ParagraphViewModel = new MarkupParagraphViewModel(string.Empty);
            RawText = markup;
        }

        public string Text
        {
            get => ParagraphViewModel.MarkupText;
            set
            {
                if (ParagraphViewModel.MarkupText != value)
                {
                    ParagraphViewModel.MarkupText = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RawText
        {
            get => _rawText;
            set
            {
                _rawText = value;
                processMarkdown();
            }
        }

        private void processMarkdown()
        {
            StringBuilder processor = new(RawText);

            MarkupExpander expander = new MarkupExpander();
            ExpansionBuilder urlExpansion = expander.BeginNewExpansion();

            for (int index = 0; index < processor.Length; index++)
            {
                processHyperlinkAtIndex(processor, expander, ref urlExpansion, index);

                // process escape characters last
            }

            expander.Expand(processor);

            Text = processor.ToString();
        }

        private static void processHyperlinkAtIndex(StringBuilder processor, MarkupExpander expander, ref ExpansionBuilder expansion, int index)
        {
            // Mark elements for replacement
            if (expansion.OnDeck == null)
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
                        index > 0 && processor[index - 1] != '\\' &&
                        index < processor.Length - 1 && processor[index + 1] == '(')
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
                         index > 0 && processor[index - 1] != '\\')
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
