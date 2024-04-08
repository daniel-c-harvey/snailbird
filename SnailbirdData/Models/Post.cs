using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Models
{
    public abstract class Post : DataAccess.IModel
    {
        public int ID { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime PostDate { get; set; } = default!;
    }
}
