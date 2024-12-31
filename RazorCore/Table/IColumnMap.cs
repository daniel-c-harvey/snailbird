namespace RazorCore.Table
{
    public interface IColumnMap<TModel>
    {
        IEnumerable<ColumnKey> Keys { get; }
        IEnumerable<string> Captions { get; }

        ITypedColumn<TModel> GetColumn(ColumnKey key);
        IColumnMap<TModel> AddColumn<TData>(ColumnKey key, IModelColumn<TModel, TData> column);
    }
}