using System.Linq.Expressions;
using Core;

namespace RazorCore
{
    public class ModelColumnBase<TModel>
    {
        public Action<TModel, object> Setter { get; }
        public Func<TModel, object> Getter { get; }

        public bool Editable { get; }

        public ModelColumnBase(Func<TModel, object> getter, Action<TModel, object> setter, bool editable = false)
        {
            //Binding = binding;
            Getter = getter;
            Setter = setter;
            Editable = editable;
        }
    }

    public class ModelColumn<TModel, TType> : ModelColumnBase<TModel>, IModelColumn<TModel, TType>
    {
        public ModelColumn(Func<TModel, TType> getter, Action<TModel, TType> setter, bool editable = false) 
            : base(getter, setter, editable) { }

        public new Action<TModel, TType> Setter { get; }
        public new Func<TModel, TType> Getter { get; }


        
    }
}
