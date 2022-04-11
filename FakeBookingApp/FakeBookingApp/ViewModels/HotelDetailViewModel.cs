using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FakeBookingApp.Models;

namespace FakeBookingApp.ViewModels
{
    public class HotelDetailViewModel : BaseViewModel
    {
        public DetailedHotelItem Item { get; set; }
        public ObservableCollection<List<Dictionary<string, string>>> Items { get; set; }

        public HotelDetailViewModel(DetailedHotelItem item = null)
        {

            Title = item?.name;
            Item = item;
        }
    }
}
