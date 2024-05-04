using System.Linq.Expressions;
using Core;

namespace RazorCore
{
    //public abstract class ModelColumnBase<TModel> : IModelColumn<TModel>
    //{
    //    protected Type Type { get; set; }
    //    public Action<TModel, string> Setter { get; }
    //    public Func<TModel, string> Getter { get; }

    //    public bool Editable { get; }

    //    protected ModelColumnBase(Type type, Func<TModel, string> getter, Action<TModel, string> setter, bool editable = false)
    //    {
    //        Type = type;
    //        Getter = getter;
    //        Setter = setter;
    //        Editable = editable;
    //    }
    //}


    public class ModelColumn<TModel> : IModelColumn<TModel>
    {
        public Action<TModel, string> Setter { get; }
        public Func<TModel, string> Getter { get; }

        public bool Editable { get; }

        public IEnumerable<string>? Choices { get; }

        public ModelColumn(Func<TModel, string> getter, Action<TModel, string> setter, bool editable = false, IEnumerable<string>? choices = null)
        {
            Getter = getter;
            Setter = setter;
            Editable = editable;
            Choices = choices;
        }
    }


    //public class ModelColumn<TModel, TType> : ModelColumnBase<TModel>
    //{ 
    //    public ModelColumn(Func<TModel, TType> getter, Action<TModel, TType> setter, bool editable = false)
    //        : base(typeof(TType), (model) => getter(model), (model, value) => setter(model, (TType)value), editable) { }

    //    public ModelColumn(ModelColumnBase<TModel> c)
    //        : base(typeof(TType), (model) => c.Getter(model), (model, value) => c.Setter(model, (TType)value), c.Editable) { }

    //    public new Action<TModel, TType> Setter => (model, value) => base.Setter(model, value);
    //    public new Func<TModel, TType> Getter => (model) => (TType)base.Getter(model);

    //}
}
