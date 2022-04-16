using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileBooking.Models;
using MobileBooking.ViewModels;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelDetailPage : ContentPage
    {
        HotelDetailViewModel viewModel;

        public HotelDetailPage(HotelDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void reservationButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new ReservationPopupPage(this.viewModel));
        }
    }
}