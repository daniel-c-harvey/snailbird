
using Core;

namespace RazorCore
{
    public interface IModelColumn<TModel>
    {
        bool Editable { get; }
        IEnumerable<string> Choices { get; }
        Func<TModel, string> Getter { get; }
        Action<TModel, string> Setter { get; }
    }
}