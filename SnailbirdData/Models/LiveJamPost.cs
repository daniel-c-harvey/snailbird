using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models.Post
{
    public class LiveJamPost : Post
    {
        public string Preamble { get; set; } = default!;
        public string VideoURL { get; set; } = default!;
        public IEnumerable<Instrument> Instruments { get; set; } = new List<Instrument>();
    }
}
