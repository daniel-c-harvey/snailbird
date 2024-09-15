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
            { false, "rc-unexpanded" },
            { true,  "rc-expanded" }
        };

        [Parameter]
        public bool Expanded { get; set; } = false;

        private string Style => ExpandedStyleMap[Expanded];

        private bool ToggleExpansion() => Expanded = !Expanded;
    }
}
