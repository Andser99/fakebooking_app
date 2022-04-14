using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileBooking.Models
{
    public class HotelItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string image_path { get; set; }
        public ImageSource Image
        {
            get
            {
                var source = new Uri(image_path);
                return source;
            }
        }
    }

    public class DetailedHotelItem : HotelItem
    {
        public List<Dictionary<string, string>> reviews { get; set; }
    }
}
