using Newtonsoft.Json.Linq;

namespace RazorCore.Table
{

    public class ModelColumn<TModel, TData> : IModelColumn<TModel, TData>
        where TData : notnull
    {
        private readonly Func<TModel, TData> _getter;
        private readonly Action<TModel, TData>? _setter;
        private readonly Func<TData, string> _formatter;
        private readonly Func<string, TData> _parser;

        public Type DataType => typeof(TData);
        public bool Editable { get; private set; }
        public bool Checkable { get; private set; }
        public Action<TModel>? ClickAction { get; private set; }

        public IEnumerable<TData>? Choices { get; private set; }

        IEnumerable<object>? ITypedColumn<TModel>.Choices => Choices?.OfType<object>();

        public ModelColumn(Func<TModel, TData> getter, Action<TModel, TData> setter)
        {
            _getter = getter;
            _setter = setter;
            Editable = false;
            Checkable = false;
            Choices = null;
            ClickAction = null;
            _formatter = value => value?.ToString() ?? string.Empty;
            _parser = str => (TData)Convert.ChangeType(str, typeof(TData));
        }

        public string Format(TData value) => _formatter(value);
        public TData Parse(string value) => _parser(value);

        public IModelColumn<TModel, TData> WithEditable()
        {
            Editable = true;
            return this;
        }

        public IModelColumn<TModel, TData> WithChoosable(IEnumerable<TData>? choices)
        {
            Choices = choices;
            return this;
        }

        public IModelColumn<TModel, TData> WithClickable(Action<TModel> action)
        {
            ClickAction = action;
            return this;
        }

        public IModelColumn<TModel, TData> WithCheckable()
        {
            Checkable = true;
            return this;
        }

        public TData Getter(TModel entity)
        {
            return _getter(entity);
        }

        public void Setter(TModel entity, TData value)
        {
            if (_setter is null)
            {
                throw new InvalidOperationException("Column is read-only");
            }
            _setter(entity, value);
        }

        public object GetValue(TModel entity)
        {
            return Getter(entity);
        }

        public void SetValue(TModel entity, object value)
        {
            if (value is TData typedValue)
            {
                Setter(entity, typedValue);
            }
            else
            {
                throw new ArgumentException($"Value must be of type {typeof(TData).Name}");
            }
        }

        string ITypedColumn<TModel>.Format(object value)
        {
            if (value is null)
            {
                return string.Empty;
            }
            else if (value is TData typedValue)
            {
                return _formatter(typedValue);
            }
            else
            {
                throw new ArgumentException($"Value must be of type {typeof(TData).Name}");
            }
        }

        object ITypedColumn<TModel>.Parse(string value)
        {
            return _parser(value);
        }
    }
}
