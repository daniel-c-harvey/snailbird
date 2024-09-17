using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore
{
    public partial class Expandable
    {
        [Parameter]
        [EditorRequired]
        public RenderFragment ChildContent { get; set; } = default!;

        private static readonly Dictionary<bool, string> ExpandedStyleMap = new Dictionary<bool, string>()
        {
            { false, "rc-expandable-unexpanded" },
            { true,  "rc-expandable-expanded" }
        };

        private static readonly Dictionary<bool, string> ExpandedTextMap = new Dictionary<bool, string>()
        {
            { false, "More" },
            { true,  "Less" }
        };

        [Parameter]
        public bool Expanded { get; set; } = false;

        private string Style => ExpandedStyleMap[Expanded];
        private string Text => ExpandedTextMap[Expanded];

        private void ToggleExpansion()
        {
            Expanded = !Expanded;
            StateHasChanged();
        }
    }
}
