using MobileBooking.Models;
using MobileBooking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationListPage : ContentPage
    {
        ReservationViewModel viewModel;
        public ReservationListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ReservationViewModel();
            viewModel.LoadItemsCommand.Execute(null);
            ReservationsCollectionView.SelectionMode = SelectionMode.Single;
            ReservationsCollectionView.SelectionChanged += ReservationsCollectionView_SelectionChanged;
        }

        private void ReservationsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0) return;
            var reservation = e.CurrentSelection.First() as ReservationItem;
            if (reservation == null)
            {
                DisplayAlert("Error", "Invalid selection", "OK");
                return;
            }

            Navigation.PushAsync(new ReviewPage(reservation));
            ReservationsCollectionView.SelectedItem = null;

        }

        //private async void reviewButton_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ReviewPage());
        //}
    }
}