using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Hotel
    {

        [Key]
        public int Id { get; set; }

        //skal vi egentlig ikke have fornavn og efternavn?
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
