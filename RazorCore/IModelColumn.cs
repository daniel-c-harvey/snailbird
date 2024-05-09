
using Core;

namespace RazorCore
{
    public interface IModelColumn<TModel>
    {
        bool Editable { get; }
        IEnumerable<string>? Choices { get; }
        Action<TModel>? ClickAction { get; }
        Func<TModel, string> Getter { get; }
        Action<TModel, string> Setter { get; }

        IModelColumn<TModel> MakeEditable();
        IModelColumn<TModel> MakeChoosable(IEnumerable<string>? choices);
        IModelColumn<TModel> MakeClickable(Action<TModel> action);
    }
}