using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models.Post
{
    public class LiveJamPost : Post<LiveJamPost>
    {
        public string Preamble { get; set; } = default!;
        public string VideoURL { get; set; } = default!;
        public IEnumerable<Instrument> Instruments { get; set; } = new List<Instrument>();

        public override LiveJamPost Clone()
        {
            return new LiveJamPost()
            {
                ID = ID,
                Title = Title,
                PostDate = PostDate,
                Preamble = Preamble,
                VideoURL = VideoURL,
                Instruments = Instruments.Select(i => i.Clone())
            };
        }
    }
}
