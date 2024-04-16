using System.Linq.Expressions;
using Core;

namespace RazorCore
{
    public class ModelColumnBase<TModel>
    {
        public Action<TModel, object> Setter { get; }
        public Func<TModel, object> Getter { get; }

        private Type type;

        public bool Editable { get; }

        public ModelColumnBase(Type type, Func<TModel, object> getter, Action<TModel, object> setter, bool editable = false)
        {
            //Binding = binding;
            Getter = getter;
            Setter = setter;
            Editable = editable;
            this.type = type;
        }
    }

    public class ModelColumn<TModel, TType> : ModelColumnBase<TModel>, IModelColumn<TModel, TType>
    {
        public ModelColumn(Func<TModel, TType> getter, Action<TModel, TType> setter, bool editable = false) 
            : base(typeof(TType), (model) => getter(model), (model, value) => setter(model, (TType)value), editable) { }
        
        public ModelColumn(ModelColumnBase<TModel> c) 
            : base(typeof(TType), (model) => c.Getter(model), (model, value) => c.Setter(model, (TType)value), c.Editable) { }

        public new Action<TModel, TType> Setter => (model, value) => base.Setter(model, value);
        public new Func<TModel, TType> Getter => (model) => (TType)base.Getter(model);



    }
}
