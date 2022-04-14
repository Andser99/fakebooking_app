using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileBooking.Models;
using MobileBooking.ViewModels;
using System.Collections.Generic;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelDetailPage : ContentPage
    {
        HotelDetailViewModel viewModel;

        public HotelDetailPage(HotelDetailViewModel viewModel)
        {
            InitializeComponent();

            Title = "Hotel Detail";
            BindingContext = this.viewModel = viewModel;
        }

        public HotelDetailPage()
        {
            InitializeComponent();

            var review = new List<Dictionary<string, string>>();
            review[0].Add("name", "velmi dobre");
            var item = new DetailedHotelItem
            {
                name = "Hotel Fatra",
                info = "Vela infa",
                image_path = "https://fakebooking.herokuapp.com/images/hotel_tatra.png",
                reviews = new List<Dictionary<string, string>>()
            };

            viewModel = new HotelDetailViewModel(item);
            BindingContext = viewModel;
        }

        private void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}