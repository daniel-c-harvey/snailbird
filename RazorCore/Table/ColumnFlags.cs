using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorCore.Table
{
    [Flags]
    public enum ColumnFlags
    {
        None = 0,
        Editable = 1,
        Choosable = 2,
        Checkable = 4,
    }
}
