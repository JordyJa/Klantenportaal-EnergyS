using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klantenportaal_EnergyS
{
    class Bericht_Type
    {
        public string Name
        {
            get;
            set;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}

