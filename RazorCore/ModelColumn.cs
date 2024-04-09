using System.Linq.Expressions;
using Core;

namespace RazorCore
{
    public class ModelColumn<TModel>
    {
        public Func<TModel, object> Binding { get; }
        public bool IsHeader { get; }

        public ModelColumn(Func<TModel, object> binding, bool isHeader = false)
        {
            Binding = binding;
            IsHeader = isHeader;
        }
    }
}
