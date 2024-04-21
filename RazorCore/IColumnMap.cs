
namespace RazorCore
{
    public interface IColumnMap<TModel>
    {
        IEnumerable<string> Captions { get; }
        IEnumerable<IModelColumn<TModel>> Columns { get; }

        IColumnMap<TModel> AddColumn(string caption, IModelColumn<TModel> column);
    }
}