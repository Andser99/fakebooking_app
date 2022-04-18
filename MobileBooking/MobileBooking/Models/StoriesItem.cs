using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileBooking.Models
{
    public class StoriesItem
    {
        public string created_at { get; set; }
        public string username { get; set; }
        public string image_path { get; set; }
        public ImageSource Image
        {
            get
            {
                var source = new Uri(Services.RestService.BaseAddress + image_path);
                return source;
            }
        }
    }
}
