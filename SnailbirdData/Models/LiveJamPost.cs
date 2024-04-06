using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models
{
    public class LiveJamPostInstrument
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class LiveJamPost : Post
    {
        public string Preamble { get; set; } = default!;
        public string VideoURL { get; set; } = default!;
        public IEnumerable<LiveJamPostInstrument> Instruments { get; set; } = default!;
    }
}
