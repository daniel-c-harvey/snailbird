using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore.Table
{
    public class ColumnKey
    {
        public required string Caption { get; set; }
        public required Type DataType { get; set; }

        public static ColumnKey Init(PropertyInfo? prop)
        {
            if (prop == null)
            {
                throw new ArgumentNullException(nameof(prop));
            }

            return Init(prop.Name, prop);
        }

        public static ColumnKey Init(string caption, PropertyInfo? prop)
        {
            if (prop == null)
            {
                throw new ArgumentNullException(nameof(prop));
            }

            return Init(caption, prop.PropertyType);
        }

        public static ColumnKey Init(string caption, Type type)
        {
            return new ColumnKey
            {
                Caption = caption,
                DataType = type
            };
        }

    }
}
