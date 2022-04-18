using MobileBooking.Services;
using MobileBooking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoriesPage : ContentPage
    {
        StoriesViewModel viewModel;
        public StoriesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StoriesViewModel();
            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void uploadStoriesButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var file = await FilePicker.PickAsync();
                var stream = await file.OpenReadAsync();
                var res = await RestService.PostStories(stream);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }
}