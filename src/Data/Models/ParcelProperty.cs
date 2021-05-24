using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funpoly.Data.Models
{
    public class ParcelProperty : BaseModel
    {
        private bool HotelBuilt { get; set; }

        #region relations

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int ParcelId { get; set; }
        public Parcel Parcel { get; set; }

        #endregion relations
    }
}