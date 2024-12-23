using NetBlocks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models.Post
{
    public class Instrument : ICloneable<Instrument>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public Instrument Clone()
        {
            return new Instrument()
            {
                Name = Name,
                Description = Description
            };
        }

        public override bool Equals(object? obj)
        {
            return obj is Instrument instrument &&
                   Name == instrument.Name &&
                   Description == instrument.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }
    }
}
