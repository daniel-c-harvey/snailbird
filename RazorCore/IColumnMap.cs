
namespace RazorCore
{
    public interface IColumnMap<TModel>
    {
        IEnumerable<string> Captions { get; }
        IEnumerable<ModelColumnBase<TModel>> Columns { get; }

        ColumnMap<TModel> AddColumn(string caption, ModelColumnBase<TModel> column);
    }
}