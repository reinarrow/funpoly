using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class BoardSquare
    {
        // Unique identifier
        public int Id { get; set; }

        // Square type from the existing types in the SquareTypes enum
        [Required]
        public SquareTypes Type { get; set; }

        // Relations
        public List<Team> Teams { get; set; }

        public Parcel Parcel { get; set; }
    }

    public enum SquareTypes
    {
        Parcel, Surprise, Game, Tax, Dices, Teleport, Border, Prize, Auction, Prison
    }
}