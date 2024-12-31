namespace RazorCore.Table
{
    //public interface IModelColumn<TModel>
    //{
    //    Func<TModel, object?> Getter { get; }
    //    Action<TModel, object?> Setter { get; }

    //    bool Editable { get; }
    //    IEnumerable<object>? Choices { get; }
    //    Action<TModel>? ClickAction { get; }
    //    bool Checkable { get; }

    //    IModelColumn<TModel> MakeEditable();
    //    IModelColumn<TModel> MakeChoosable(IEnumerable<object> choices);
    //    IModelColumn<TModel> MakeClickable(Action<TModel> action);
    //    IModelColumn<TModel> MakeCheckable();
    //}

    public interface IModelColumn<TModel, TData>
    {
        Func<TModel, TData> Getter { get; }
        Action<TModel, TData> Setter { get; }

        bool Editable { get; }
        IEnumerable<TData>? Choices { get; }
        Action<TModel>? ClickAction { get; }
        bool Checkable { get; }

        IModelColumn<TModel, TData> MakeEditable();
        IModelColumn<TModel, TData> MakeChoosable(IEnumerable<object> choices);
        IModelColumn<TModel, TData> MakeClickable(Action<TModel> action);
        IModelColumn<TModel, TData> MakeCheckable();
    }
}