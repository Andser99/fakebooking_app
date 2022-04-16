using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Xaml;
using MobileBooking.ViewModels;
using MobileBooking.Services;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationPopupPage : PopupPage
    {
        HotelDetailViewModel hotelViewModel;
        DateTime date_from, date_to;
        public ReservationPopupPage(HotelDetailViewModel viewModel)
        {
            hotelViewModel = viewModel;
            date_to = DateTime.Now;
            date_from = DateTime.Now;
            InitializeComponent();
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void ConfirmReservationButton_Clicked(object sender, EventArgs e)
        {
            var res = await RestService.PostReservations(User.CurrentUser.UserId.ToString(), hotelViewModel.Hotel.id, date_from.ToString("yyyy-MM-dd"), date_to.ToString("yyyy-MM-dd"));
            if (res.ResultMessage == "Success")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Success", "Reservation created", "Ok");
                });
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Error", res.ResultMessage, "Ok");
                });
            }

        }

        private void FromDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if ((e.NewDate - DateTime.Now).Days >= 0)
            {
                date_from = e.NewDate;
            } 
            else
            {
                DisplayAlert("Error", "Can't select a date from the past", "Ok");
                FromDatePicker.Date = DateTime.Now;
            }
        }

        private void ToDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if ((e.NewDate - DateTime.Now).Days >= 0)
            {
                date_to = e.NewDate;
            }
            else
            {
                DisplayAlert("Error", "Can't select a date from the past", "Ok");
                ToDatePicker.Date = DateTime.Now;
            }
        }
    }
}