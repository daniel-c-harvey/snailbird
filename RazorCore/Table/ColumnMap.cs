using System.Reflection;

namespace RazorCore.Table
{
    public class ColumnMap<TModel> : IColumnMap<TModel>
    {
        protected IDictionary<string, IModelColumn<TModel>> columns = new Dictionary<string, IModelColumn<TModel>>();
        protected IDictionary<string, Type> types = new Dictionary<string, Type>();

        public IEnumerable<string> Captions => types.Keys;
        public IEnumerable<ColumnKey> Keys => types.Select(c => new ColumnKey { Caption = c.Key, DataType = c.Value });

        public IModelColumn<TModel> GetColumn(ColumnKey key)
        {
            return columns[key.Caption];
        }
        
        public IColumnMap<TModel> AddColumn(ColumnKey key, IModelColumn<TModel> column)
        {
            types.Add(key.Caption, key.DataType);
            columns.Add(key.Caption, column);

            return this;
        }
    }
}
