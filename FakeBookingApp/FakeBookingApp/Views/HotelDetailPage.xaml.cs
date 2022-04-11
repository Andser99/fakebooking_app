using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FakeBookingApp.Models;
using FakeBookingApp.ViewModels;

namespace FakeBookingApp.Views
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

        public HotelDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new HotelDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}