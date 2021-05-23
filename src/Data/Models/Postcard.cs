using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funpoly.Data.Models
{
    public class Postcard : BaseModel
    {
        // Relations
        public int ParcelId { get; set; }

        public Parcel Parcel { get; set; }

        public List<Team> Teams { get; set; }
    }
}