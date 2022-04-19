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
    public class ReservationViewModel : BaseViewModel
    {
        public ObservableCollection<ReservationItem> Reservations { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ReservationViewModel()
        {
            Title = "Reservations";
            Reservations = new ObservableCollection<ReservationItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        { 
            try
            {
                Reservations.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                var reservation_items = await RestService.GetReservations();
                if (reservation_items != null)
                {
                    foreach (var item in reservation_items)
                    {
                        item.hotel_name = await RestService.GetHotelName(item.hotel_id);
                        item.reserved_from = item.reserved_from.Substring(0, 10);
                        item.reserved_to = item.reserved_to.Substring(0, 10);
                        Reservations.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
