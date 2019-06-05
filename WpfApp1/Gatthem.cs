using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Gatthem
    {
        public int gattHemID { get; set; }
        public bool gattHem { get; set; }
        public int barnID { get; set; }
        public int personalID { get; set; }

        public override string ToString()
        {
            return "Barn med IDnr: " + barnID + " har gått hem: " + gattHem + ". Registrerat av: " + personalID;
        }
    }
}
