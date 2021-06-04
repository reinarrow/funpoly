using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funpoly.Data.Models
{
    public class PostcardTeam : BaseModel
    {
        #region relations

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int PostcardId { get; set; }
        public Postcard Postcard { get; set; }

        #endregion relations
    }
}