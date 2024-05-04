using System.Linq.Expressions;
using Core;

namespace RazorCore
{

    public class ModelColumn<TModel> : IModelColumn<TModel>
    {
        public Action<TModel, string> Setter { get; }
        public Func<TModel, string> Getter { get; }

        public bool Editable { get; private set; }

        public IEnumerable<string>? Choices { get; private set; }

        public ModelColumn(Func<TModel, string> getter, Action<TModel, string> setter)
        {
            Getter = getter;
            Setter = setter;
            Editable = false;
            Choices = null;
        }

        public ModelColumn<TModel> MakeEditable()
        {
            Editable = true;
            return this;
        }

        public ModelColumn<TModel> MakeChoosable(IEnumerable<string>? choices)
        {
            Choices = choices;
            return this;
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
