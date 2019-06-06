using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Personal
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int avdelning { get; set; }


        public override string ToString()
        {
            return firstname.ToUpper() + " " + lastname.ToUpper() + " - Avdelning " + avdelning;
        }
    }
}
