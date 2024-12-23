using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore.Markup
{
    public class MarkupParagraphViewModel
    {
        public string MarkupText { get; set; } = default!;

        public MarkupParagraphViewModel(string text)
        {
            MarkupText = text;
        }

    }
}
