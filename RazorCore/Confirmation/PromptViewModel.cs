using Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore.Confirmation
{
    public class PromptModel
    {
        public string HeaderText { get; }
        public string BodyText { get; }

        public PromptModel( string headerText, string bodyText)
        {
            HeaderText = headerText;
            BodyText = bodyText;
        }
    }

    public class PromptViewModel
    {
        public PromptModel? Prompt { get; set; }
        public Action<ConfirmEventArgs>? OnClose { get; set; }

        public Func<bool>? PromptCondition { get; set; }

        public bool IsConfigured => Prompt != null;

        public void Reset()
        {
            Prompt = null;
            OnClose = null;
            PromptCondition = null;
        }
    }
}
