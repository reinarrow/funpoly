using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Parcel
    {
        // Unique identifier
        public int Id { get; set; }

        // Name of place and location
        public string Name { get; set; }

        // Parcel acquisition price in euros
        [Required]
        public int Price { get; set; }

        // Parcel tax to be paid when no hotel is constructed
        [Required]
        public int RawTax { get; set; }

        // Parcel tax to be paid when a hotel is constructed
        [Required]
        public int HotelTax { get; set; }

        // Bool for hotel being built in the parcel
        public bool HotelBuilt { get; set; }

        // Relations
        public Postcard Postcard { get; set; }

        public int ContinentId { get; set; }
        public Continent Continent { get; set; }
        public BoardSquare BoardSquare { get; set; }
        public int BoardSquareId { get; set; }

        public Team Team { get; set; }
        public int TeamId { get; set; }
    }
}