using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funpoly.Data.Models
{
    public class ParcelProperty : BaseModel
    {
        public bool HotelBuilt { get; set; }

        public bool WifiServiceAvailable { get; set; }
        public bool BuffetServiceAvailable { get; set; }
        public bool ParkingServiceAvailable { get; set; }
        public bool PoolServiceAvailable { get; set; }

        #region relations

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int ParcelId { get; set; }
        public Parcel Parcel { get; set; }

        #endregion relations

        public ParcelProperty ShallowCopy()
        {
            return (ParcelProperty)MemberwiseClone();
        }
    }
}