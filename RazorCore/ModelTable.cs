using System.Linq.Expressions;
using Core;

namespace RazorCore
{
    public class ModelTable<TModel>
    {
        public IEnumerable<TModel> Models { get; }
        private Dictionary<string, ModelColumn<TModel>> _columnMap;
        public IEnumerable<string> Captions => _columnMap.Keys;
        public IEnumerable<ModelColumn<TModel>> Columns => _columnMap.Values;

        public ModelTable(IEnumerable<TModel> models, Dictionary<string, ModelColumn<TModel>> columnMap)
        {
            Models = models;
            _columnMap = columnMap;
        }


    }
}
