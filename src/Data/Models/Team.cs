using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Team : BaseModel
    {
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

        // Number of consecutive sixes obtained in dices. Used for bids and jail punishments
        public int ConsecutiveSixes { get; set; }

        // Bool indicating if the team is currently in prison
        public bool InPrison { get; set; }

        // Turns that the team has been in prison. 0 if the team is not in prison at the moment
        public int TurnsInPrison { get; set; }

        #region relations

        // Game instance
        public int GameId { get; set; }

        public Game Game { get; set; }

        public List<Player> Players { get; set; }

        // Current position of the team in the game board
        public int BoardSquareId { get; set; }

        public BoardSquare BoardSquare { get; set; }

        // Postcards owned by the team
        public List<PostcardTeam> PostcardTeams { get; set; }

        // Transport of the team in the current lap
        public int? TransportId { get; set; }

        public Transport Transport { get; set; }

        // List of parcel properties of the team
        public List<ParcelProperty> ParcelProperties { get; set; }

        #endregion relations

        public Team ShallowCopy()
        {
            return (Team)MemberwiseClone();
        }
    }
}