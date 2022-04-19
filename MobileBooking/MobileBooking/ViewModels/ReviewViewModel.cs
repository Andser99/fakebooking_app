using MobileBooking.Models;
using MobileBooking.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileBooking.ViewModels
{
    public class ReviewViewModel : BaseViewModel
    {
        public string reserved_from { get; set; }
        public string reserved_to { get; set; }
        public string review_text { get; set; }
        public string hotel_name { get; set; }
        public int stars { get; set; }
        public ReviewViewModel()
        {
            Title = "My Review";
        }
    }
}
