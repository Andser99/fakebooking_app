using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FakeBookingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        bool IsBusy = false;
        public LoginPage()
        {
            InitializeComponent();
            LoginButton.Command = new Command(Login);
        }

        public async void Login()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            var res = await User.Login(UsernameEntry.Text, PasswordEntry.Text);
            if (res == "Success")
            {
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Error", res, "Ok");
                });
            }
            IsBusy = false;
        }

        protected override void OnAppearing()
        {
            User.CurrentUser = null;
        }

    }
}