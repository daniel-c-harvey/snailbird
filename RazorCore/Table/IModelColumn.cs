namespace RazorCore.Table
{
    public interface IModelColumn<TModel>
    {
        Func<TModel, object> Getter { get; }
        Action<TModel, object> Setter { get; }

        bool Editable { get; }
        IEnumerable<object>? Choices { get; }
        Action<TModel>? ClickAction { get; }
        bool Checkable { get; }

        IModelColumn<TModel> MakeEditable();
        IModelColumn<TModel> MakeChoosable(IEnumerable<object> choices);
        IModelColumn<TModel> MakeClickable(Action<TModel> action);
        IModelColumn<TModel> MakeCheckable();
    }
}