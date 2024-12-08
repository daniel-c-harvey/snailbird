namespace RazorCore.Messages
{
    public abstract class MessageBase<TAction>
    {
        public TAction Action { get; }

        public MessageBase(TAction action)
        {
            Action = action;
        }
    }
}
