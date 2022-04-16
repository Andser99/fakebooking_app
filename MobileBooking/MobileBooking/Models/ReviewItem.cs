using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBooking.Models
{
    public class ReviewItem
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string hotel_id { get; set; }
        public string username { get; set; }
        public string text { get; set; }
        public string stars { get; set; }
    }
}
