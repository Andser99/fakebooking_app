using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MobileBooking.Models;

namespace MobileBooking.ViewModels
{
    public class HotelDetailViewModel : BaseViewModel
    {
        public DetailedHotelItem Hotel { get; set; }
        public ObservableCollection<ReviewItem> Reviews { get; set; }

        public HotelDetailViewModel(DetailedHotelItem hotel = null)
        {
            Title = hotel?.name;
            Hotel = hotel;
            Reviews = new ObservableCollection<ReviewItem>(hotel.reviews);
        }
    }
}
