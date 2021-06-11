using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Player
    {
        // Unique identifier
        public int Id { get; set; }

        // Player name
        [Required]
        public string Name { get; set; }

        // Bool indicating if the player is the captain of his/her team
        public bool Captain { get; set; }

        //Relations
        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}