using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Country
    {
        public int? Id { get; set; } // Kunstig primærnøgle identity

        public string Iso2Code { get; set; } // Naturlig nøgle (unik)  fx DK
        public string Name { get; set; } // fx. Denmark
        public string Nationality { get; set; } //fx. danish 
    }
}
