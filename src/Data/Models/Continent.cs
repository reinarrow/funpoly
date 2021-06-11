using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Continent
    {
        // Unique identifier
        public int Id { get; set; }

        // Continent name
        [Required]
        public string Name { get; set; }

        // Relations

        public List<Parcel> Parcels { get; set; }
    }
}