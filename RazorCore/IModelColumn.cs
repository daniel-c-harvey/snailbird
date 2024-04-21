
namespace RazorCore
{
    public interface IModelColumn<TModel>
    {
        bool Editable { get; }
        Func<TModel, string> Getter { get; }
        Action<TModel, string> Setter { get; }
    }
}