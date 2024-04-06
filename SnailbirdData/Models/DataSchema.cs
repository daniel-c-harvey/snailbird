using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models
{
    public class DataSchema
    {
        public string Collection { get; }

        public DataSchema(string collection)
        {
            Collection = collection;
        }
    }
}
