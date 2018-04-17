using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vragen_en_klachten1
{
    class Bericht_Type
    {
        /// <summary>
        ///  setting the item names for the VragenKlachten dropdown menu
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// override for getting the item's string value
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

