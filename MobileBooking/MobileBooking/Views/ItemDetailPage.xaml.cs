using MobileBooking.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MobileBooking.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}