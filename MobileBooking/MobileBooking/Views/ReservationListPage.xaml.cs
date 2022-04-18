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
        }

        private async void reviewButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReviewPage());
        }
    }
}