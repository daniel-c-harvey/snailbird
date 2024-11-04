using Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore.Confirmation
{
    public class ConfirmationModel
    {
        public string HeaderText { get; }
        public string BodyText { get; }
        public Func<bool>? PromptCondition { get; }

        public ConfirmationModel( string headerText, string bodyText, Func<bool>? promptCondition = null)
        {
            HeaderText = headerText;
            BodyText = bodyText;
            PromptCondition = promptCondition;
        }
    }

    public class ConfirmationViewModel
    {
        public ConfirmationModel? Model { get; set; }
        public Action<ConfirmEventArgs>? OnClose { get; set; }

        public bool IsConfigured => Model != null;

        public void Reset()
        {
            Model = null;
            OnClose = null;
        }
    }
}
