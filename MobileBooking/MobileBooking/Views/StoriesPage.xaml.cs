using MobileBooking.Services;
using MobileBooking.ViewModels;
using Newtonsoft.Json;
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
                if (file != null)
                {
                    var stream = await file.OpenReadAsync();
                    var res = await RestService.PostStories(stream);

                    var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
                    if (deserialized.ContainsKey("message"))
                    {
                        await DisplayAlert("Success", deserialized["message"], "OK");
                    }
                    else if (deserialized.ContainsKey("error"))
                    {
                        await DisplayAlert("Error", deserialized["error"], "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Something gone wrong", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

        }
    }
}