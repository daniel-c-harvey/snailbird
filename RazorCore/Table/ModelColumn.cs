namespace RazorCore.Table
{

    public class ModelColumn<TModel> : IModelColumn<TModel>
    {
        public Action<TModel, object> Setter { get; }
        public Func<TModel, object> Getter { get; }

        public bool Editable { get; private set; }
        public bool Checkable { get; private set; }
        public IEnumerable<object>? Choices { get; private set; }
        public Action<TModel>? ClickAction { get; private set; }

        public static ModelColumn<TModel> Init<T>(Func<TModel, T> getter, Action<TModel, T> setter)
        {
            return new ModelColumn<TModel>((m) => getter(m), (m, value) => setter(m, (T)value));
        }

        private ModelColumn(Func<TModel, object> getter, Action<TModel, object> setter)
        {
            Getter = getter;
            Setter = setter;
            Editable = false;
            Checkable = false;
            Choices = null;
            ClickAction = null;
        }

        public IModelColumn<TModel> MakeEditable()
        {
            Editable = true;
            return this;
        }

        public IModelColumn<TModel> MakeChoosable(IEnumerable<object>? choices)
        {
            Choices = choices;
            return this;
        }

        public IModelColumn<TModel> MakeClickable(Action<TModel> action)
        {
            ClickAction = action;
            return this;
        }

        public IModelColumn<TModel> MakeCheckable()
        {
            Checkable = true;
            return this;
        }
    }
}
