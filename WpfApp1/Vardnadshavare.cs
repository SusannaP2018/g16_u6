﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Vardnadshavare
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }

        public override string ToString()
        {
            return Id + " " + FirstName.ToUpper() + " " + LastName.ToUpper() + " " + "Telefonnr: " + Telephone;
        }
    }
}
