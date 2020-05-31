using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapiclient2.Models
{
    public class Bookings
    {
        public int BookingId { get; set; }
        public int FlightNo { get; set; }
        public int PassengerId { get; set; }
        public int Price { get; set; }
    }
}
