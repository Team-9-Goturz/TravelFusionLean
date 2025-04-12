using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }

        public string From; 
        public string To;
        public decimal Price;
        public Currency Currency;
    }
}
