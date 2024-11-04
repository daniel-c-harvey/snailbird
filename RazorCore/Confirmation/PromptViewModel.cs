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
        public Func<bool>? PromptCondition { get; }

        public PromptModel( string headerText, string bodyText, Func<bool>? promptCondition = null)
        {
            HeaderText = headerText;
            BodyText = bodyText;
            PromptCondition = promptCondition;
        }
    }

    public class PromptViewModel
    {
        public PromptModel? Model { get; set; }
        public Action<ConfirmEventArgs>? OnClose { get; set; }

        public bool IsConfigured => Model != null;

        public void Reset()
        {
            Model = null;
            OnClose = null;
        }
    }
}
