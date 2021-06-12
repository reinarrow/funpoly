using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Parcel : BaseModel
    {
        // Name of place and location
        public string Name { get; set; }

        // Parcel acquisition price in euros
        [Required]
        public decimal Price { get; set; }

        // Hotel construction price in euros
        [Required]
        public decimal HotelPrice { get; set; }

        #region relations

        // Postcard that is acquired in the parcel
        public Postcard Postcard { get; set; }

        // Continent that the parcel belongs to
        public int ContinentId { get; set; }

        public Continent Continent { get; set; }

        // Position of the parcel in the game board
        public BoardSquare BoardSquare { get; set; }

        public int BoardSquareId { get; set; }

        // Parcel property for the acquisition by teams
        public List<ParcelProperty> ParcelProperties { get; set; }

        #endregion relations
    }
}