namespace RazorCore.Table
{
    public interface IColumnMap<TModel>
    {
        IEnumerable<ColumnKey> Keys { get; }
        IEnumerable<string> Captions { get; }

        IModelColumn<TModel> GetColumn(ColumnKey key);
        IColumnMap<TModel> AddColumn(ColumnKey key, IModelColumn<TModel> column);
    }
}