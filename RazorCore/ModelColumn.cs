namespace RazorCore
{

    public class ModelColumn<TModel> : IModelColumn<TModel>
    {
        public Action<TModel, string> Setter { get; }
        public Func<TModel, string> Getter { get; }

        public bool Editable { get; private set; }

        public IEnumerable<string>? Choices { get; private set; }

        public Action<TModel>? ClickAction { get; private set; }

        public ModelColumn(Func<TModel, string> getter, Action<TModel, string> setter)
        {
            Getter = getter;
            Setter = setter;
            Editable = false;
            Choices = null;
            ClickAction = null;
        }

        public IModelColumn<TModel> MakeEditable()
        {
            Editable = true;
            return this;
        }

        public IModelColumn<TModel> MakeChoosable(IEnumerable<string>? choices)
        {
            Choices = choices;
            return this;
        }

        public IModelColumn<TModel> MakeClickable(Action<TModel> action)
        {
            ClickAction = action;
            return this;
        }
    }
}
