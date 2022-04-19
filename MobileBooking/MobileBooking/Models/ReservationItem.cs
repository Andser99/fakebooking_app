using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBooking.Models
{
    public class ReservationItem
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string hotel_id { get; set; }
        public string review_id { get; set; } = null;
        public string hotel_name { get; set; }
        public string reserved_from { get; set; }
        public string reserved_to { get; set; }
    }
}
