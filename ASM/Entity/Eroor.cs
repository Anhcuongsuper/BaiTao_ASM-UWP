using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Entity
{
    class Eroor
    {
        public int status { get; set; }

        public string message { get; set; }

        public Dictionary<string, string> error { get; set; }
    }
}
