
namespace RazorCore
{
    public class ColumnMap<TModel> : IColumnMap<TModel>
    {
        protected IList<string> captions;
        protected IList<ModelColumnBase<TModel>> columns;

        public IEnumerable<string> Captions => captions;
        public IEnumerable<ModelColumnBase<TModel>> Columns => columns;
        public ColumnMap()
        {
            captions = new List<string>();
            columns = new List<ModelColumnBase<TModel>>();
        }

        public ColumnMap<TModel> AddColumn(string caption, ModelColumnBase<TModel> column)
        {
            captions.Add(caption);
            columns.Add(column);

            return this;
        }
    }
}
