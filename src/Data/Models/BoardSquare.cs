using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class BoardSquare : BaseModel
    {
        // Square type from the existing types in the SquareTypes enum
        [Required]
        public SquareTypes Type { get; set; }

        #region relations

        public List<Team> Teams { get; set; }

        public Parcel Parcel { get; set; }

        #endregion relations
    }

    public enum SquareTypes
    {
        Parcel, Surprise, Game, Tax, Dices, Teleport, Border, Prize, Auction, Prison
    }
}