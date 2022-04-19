using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileBooking.Models;
using MobileBooking.Views;
using MobileBooking.ViewModels;
using MobileBooking.Services;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        ItemsViewModel viewModel;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as HotelItem;
            if (item == null)
                return;

            var res = await RestService.GetHotel(item);
            await Navigation.PushAsync(new HotelDetailPage(new HotelDetailViewModel(res)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}