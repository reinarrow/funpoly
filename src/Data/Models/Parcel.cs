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
        public int Price { get; set; }

        // Hotel construction price in euros
        [Required]
        public int HotelPrice { get; set; }

        // Parcel tax to be paid when no hotel is constructed
        [Required]
        public int RawTax { get; set; }

        // Parcel tax to be paid when a hotel is constructed
        [Required]
        public int HotelTax { get; set; }

        // Parcel tax to be paid when two hotels are constructed on the same continent
        [Required]
        public int TwoHotelsTax { get; set; }

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
        public ParcelProperty ParcelProperty { get; set; }

        #endregion relations
    }
}