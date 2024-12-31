using System.Reflection;

namespace RazorCore.Table
{
    public class ColumnMap<TModel> : IColumnMap<TModel>
    {
        protected IDictionary<string, ITypedColumn<TModel>> columns = new Dictionary<string, ITypedColumn<TModel>>();
        protected IDictionary<string, Type> types = new Dictionary<string, Type>();

        public IEnumerable<string> Captions => types.Keys;
        public IEnumerable<ColumnKey> Keys => types.Select(c => new ColumnKey { Caption = c.Key, DataType = c.Value });

        public ITypedColumn<TModel> GetColumn(ColumnKey key)
        {
            if (columns.TryGetValue(key.Caption, out var column))
            {
                if (column.DataType != key.DataType)
                    throw new InvalidOperationException($"Column type mismatch. Expected {key.DataType.Name} but got {column.DataType.Name}");
                return column;
            }
            throw new KeyNotFoundException($"Column {key.Caption} not found");
        }
        
        public IColumnMap<TModel> AddColumn<TData>(ColumnKey key, IModelColumn<TModel, TData> column)
        {
            types.Add(key.Caption, key.DataType);
            columns.Add(key.Caption, column);

            return this;
        }
    }
}
