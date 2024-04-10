using System.Linq.Expressions;
using Core;

namespace RazorCore
{
    public class ModelColumn<TModel, TType> : IModelColumn<TModel, TType>
    {
        //public Func<TModel, object> Binding { get; }
        public Action<TModel, TType> Setter { get; }
        public Func<TModel, TType> Getter { get; }

        public bool Editable { get; }

        public ModelColumn(Func<TModel, TType> getter, Action<TModel, TType> setter, bool editable = false)
        {
            //Binding = binding;
            Getter = getter;
            Setter = setter;
            Editable = editable;
        }
    }
}
