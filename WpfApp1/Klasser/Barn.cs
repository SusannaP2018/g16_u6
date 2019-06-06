using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Barn
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Lokal { get; set; }
        public int Avdelning { get; set; }

        public override string ToString()
        {
            return "IDnr: " + Id + ". " + FirstName.ToUpper() + " " + LastName.ToUpper() + " Lokal: " + Lokal + " Avdelning: " + Avdelning;
        }
    }
}
