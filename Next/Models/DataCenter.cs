using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Next.Models
{
    public class DataCenter
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public ICollection<Server> Servers { get; set; }
    }
}
