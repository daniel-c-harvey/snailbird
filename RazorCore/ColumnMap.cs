
namespace RazorCore
{
    public class ColumnMap<TModel> : IColumnMap<TModel>
    {
        protected IList<string> captions;
        protected IList<IModelColumn<TModel>> columns;

        public IEnumerable<string> Captions => captions;
        public IEnumerable<IModelColumn<TModel>> Columns => columns;
        public ColumnMap()
        {
            captions = new List<string>();
            columns = new List<IModelColumn<TModel>>();
        }

        public IColumnMap<TModel> AddColumn(string caption, IModelColumn<TModel> column)
        {
            captions.Add(caption);
            columns.Add(column);

            return this;
        }
    }
}
