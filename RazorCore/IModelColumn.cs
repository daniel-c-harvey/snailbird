
namespace RazorCore
{
    public interface IModelColumn<TModel, TType>
    {
        bool Editable { get; }
        Func<TModel, TType> Getter { get; }
        Action<TModel, TType> Setter { get; }
    }
}