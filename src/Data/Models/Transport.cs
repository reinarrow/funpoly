using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Transport : BaseModel
    {       
        // Common name of the transport
        [Required]
        public string Name { get; set; }

        // Number of dices to be thrown when using this transport
        [Required]
        public int Dices { get; set; }

        // Number to be substracted per dice when using this transport
        [Required]
        public int Substraction { get; set; }

        // Relations
        public Team Team { get; set; }
    }
}