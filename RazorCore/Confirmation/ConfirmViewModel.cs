using Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RazorCore.Confirmation
{
    public class PromptMessage
    {
        public string HeaderText { get; }
        public string BodyText { get; }

        public PromptMessage( string headerText, string bodyText)
        {
            HeaderText = headerText;
            BodyText = bodyText;
        }
    }

    public abstract class ModalViewModel
    {
        public PromptMessage? PromptMessage { get; set; }
        public bool IsConfigured => PromptMessage != null;

        public virtual void Reset()
        {
            PromptMessage = null;
        }
    }

    public class ConfirmViewModel : ModalViewModel
    {
        
        public Action<ConfirmEventArgs>? OnClose { get; set; }

        public override void Reset()
        {
            base.Reset();
            OnClose = null;
        }
    }

    public class PromptChoice
    {
        public int ID { get; }
        public string Label { get; }
        public string CssClass { get; }

        public PromptChoice(int id, string label, string cssClass)
        {
            ID = id;
            Label = label;
            CssClass = cssClass;
        }
    }


    public class PromptViewModel : ModalViewModel
    {
        public Action<ResultEventArgs<PromptChoice>>? OnClose { get; set; }
        public IEnumerable<PromptChoice> Choices { get; }

        public PromptViewModel(IEnumerable<PromptChoice> choices)
        {
            Choices = choices;
        }

        public override void Reset()
        {
            base.Reset();
            OnClose = null;
        }
    }
}
