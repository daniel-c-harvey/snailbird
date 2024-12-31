using Newtonsoft.Json.Linq;

namespace RazorCore.Table
{
    public interface ITypedColumn<TModel>
    {
        Type DataType { get; }
        object GetValue(TModel entity);
        void SetValue(TModel entity, object value);
        string Format(object value);
        object Parse(string value);
        bool Editable { get; }
        IEnumerable<object>? Choices { get; }
        Action<TModel>? ClickAction { get; }
        bool Checkable { get; }
    }

    public interface IModelColumn<TModel, TData> : ITypedColumn<TModel>
    {
        TData Getter(TModel entity);
        void Setter(TModel entity, TData value);
        new IEnumerable<TData>? Choices { get; }
        string Format(TData value);
        new TData Parse(string value);
        IModelColumn<TModel, TData> WithEditable();
        IModelColumn<TModel, TData> WithChoosable(IEnumerable<TData> choices);
        IModelColumn<TModel, TData> WithClickable(Action<TModel> action);
        IModelColumn<TModel, TData> WithCheckable();
    }
}