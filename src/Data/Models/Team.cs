using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Team
    {
        // Unique identifier
        public int Id { get; set; }

        // Team name
        [Required]
        public string Name { get; set; }

        // Left team cash in Euros
        [Required]
        public decimal Cash { get; set; }

        // Hex indicating the team color for displaying purposes
        [Required]
        public string Color { get; set; }

        // Turn order for the movements
        [Required]
        public int Turn { get; set; }

        // Days employed in the world tours
        public int Days { get; set; }

        // Number of consecutive twelves obtained in dices. Used for bids and jail punishments
        public int ConsecutiveTwelves { get; set; }

        // Bool indicating if the team is currently in prison
        public bool InPrison { get; set; }

        // Turns that the team has been in prison. 0 if the team is not in prison at the moment
        public int TurnsInPrison { get; set; }

        // Relations
        public List<Player> Players { get; set; }

        public int BoardSquareId { get; set; }
        public BoardSquare BoardSquare { get; set; }
        public List<Postcard> Postcards { get; set; }
        public int? TransportId { get; set; }
        public Transport Transport { get; set; }
        public List<Parcel> Parcels { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}