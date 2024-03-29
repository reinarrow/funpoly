﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Funpoly.Data.Models
{
    public class Continent : BaseModel
    {
        // Continent name
        [Required]
        public string Name { get; set; }

        #region relations

        public List<Parcel> Parcels { get; set; }

        #endregion relations
    }
}