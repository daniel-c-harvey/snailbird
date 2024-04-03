using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models
{
    public class LiveJamPostInstrument
    {
        public string Name { get; }
        public string Description { get; }
        public LiveJamPostInstrument(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    public class LiveJamPost : Post
    {
        public string Preamble { get; }
        public string VideoURL { get; }
        public IEnumerable<LiveJamPostInstrument> Instruments { get; }
        public LiveJamPost(int id, 
                           string title, 
                           DateTime postDate, 
                           string preamble, 
                           string videoURL, 
                           IEnumerable<LiveJamPostInstrument> instruments)
            : base(id, title, postDate)
        {
            Preamble = preamble;
            VideoURL = videoURL;
            Instruments = instruments;
        }
    }
}
