using System.Linq.Expressions;
using Core;

namespace RazorCore
{
    public class ModelColumn<TModel>
    {
        public Func<TModel, object> Binding { get; }

        public bool Editable { get; }

        public ModelColumn(Func<TModel, object> binding, bool editable = false)
        {
            Binding = binding;
            Editable = editable;
        }
    }
}
