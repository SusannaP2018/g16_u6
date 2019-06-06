using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Schema
    {
        public DateTime? Narvarodag { get; set; } 
        public DateTime? LedigDag { get; set; }
        public DateTime? Sjukdag { get; set; }
        public bool Frukost { get; set; }
        public string Far_hamta { get; set; }
        public int Barn_id { get; set; }

        
        
        public override string ToString()
        {
            return Narvarodag + " " + LedigDag + " " + Sjukdag + " " + Frukost + " " + Far_hamta + " " + Barn_id;
        }
        



    }
}
