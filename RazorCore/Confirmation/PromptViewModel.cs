using NetBlocks.Utilities;
using static NetBlocks.Models.Event;

namespace RazorCore.Confirmation
{

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

        public override bool Equals(object? obj)
        {
            return obj is PromptChoice choice &&
                   ID == choice.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }
    }

    public class PromptViewModel : ModalViewModel
    {
        public Dictionary<PromptChoice, EventBase?> Choices { get; }

        public PromptViewModel(IEnumerable<PromptChoice> choices)
        {
            Choices = new(choices.Select(choice => new KeyValuePair<PromptChoice, EventBase?>(choice, null)));
        }

        public override void Reset()
        {
            base.Reset();
            Choices.Values.Apply(value => value = null);
        }
    }
}
