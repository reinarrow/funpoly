using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funpoly.Data.Models
{
    public class Settings : BaseModel
    {
        // OK Cash of the teams at the start of the game
        public decimal InitialCash { get; set; }

        // OK Amount paid to the teams on completing a lap
        public decimal LapPaymentAmount { get; set; }

        // OK Tax to be payed by teams on a speed fine event
        public decimal SpeedFineTax { get; set; }

        // OK Number of reprimands to send player to prison
        public int SpeedReprimandCount { get; set; }

        // Raw Property tax relation to price. For instance if a property costs 100, the raw tax will be this multiplier per 100 (default 0.5 -> 50)
        public decimal RawPropertyTaxPriceMultiplier { get; set; }

        // Multiplier of the property price
        public decimal RepurchaseMultiplier { get; set; }

        // Multiplier for the property tax when the teams owns the four properties of the continent
        public decimal FourPropertiesTaxMultiplier { get; set; }

        // Multiplier of the property tax over raw when a hotel is built
        public decimal HotelPropertyTaxMultiplier { get; set; }

        // Increment of the property tax per available service (wifi, buffet, parking and pool)
        public decimal ServicePropertyTaxIncrement { get; set; }
    }
}