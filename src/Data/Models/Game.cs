using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Game
    {
        // Unique identifier
        public int Id { get; set; }
        public string Name { get; set; }

        // Game Status
        [Required]
        public GameStatus Status { get; set; }

        #region relations

        public List<Team> Teams { get; set; }

        #endregion
    }

    public enum GameStatus
    {
        NotStarted, TeamsConfig, OnGoing, Finished
    }
}