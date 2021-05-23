using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Player : BaseModel
    {
        // Player name
        [Required]
        public string Name { get; set; }

        // Bool indicating if the player is the captain of his/her team
        public bool Captain { get; set; }

        #region relations

        public int TeamId { get; set; }

        public Team Team { get; set; }

        #endregion relations
    }
}