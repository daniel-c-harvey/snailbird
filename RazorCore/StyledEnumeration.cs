using NetBlocks.Utilities;

namespace RazorCore
{
    public class StyledEnumeration : Enumeration
    {
        public string CssClass { get; set; }

        public StyledEnumeration(int id, string name, string cssClass) : base(id, name)
        {
            CssClass = cssClass;
        }
    }
}
