using Core;
using RazorCore.Confirmation;

namespace RazorCore.Navigation
{
    public class NavigatePromptChoices : Enumeration
    {
        public static NavigatePromptChoices Cancel = new(1, "Cancel", "btn-outline-danger");
        public static NavigatePromptChoices Discard = new(2, "Discard", "btn-outline-secondary");
        public static NavigatePromptChoices Save = new(3, "Save", "btn-primary");

        public PromptChoice Choice { get; set; }

        public NavigatePromptChoices(int iID, string sName, string cssClass) : base(iID, sName) 
        {
            Choice = new(iID, sName, cssClass);
        }
    }
}
