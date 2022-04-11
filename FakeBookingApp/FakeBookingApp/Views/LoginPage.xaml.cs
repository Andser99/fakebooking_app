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
        }

        protected override void OnAppearing()
        {
            User.CurrentUser = null;
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            var res = await User.Register(UsernameEntry.Text, PasswordEntry.Text, PasswordAgainEntry.Text);
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

        private async void LoginButton_Clicked(object sender, EventArgs e)
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

        private void SelectLoginButton_Clicked(object sender, EventArgs e)
        {
            SelectLoginButton.IsVisible = false;
            SelectRegisterButton.IsVisible = false;
            UsernameEntry.IsVisible = true;
            PasswordEntry.IsVisible = true;
            BackToSelectButton.IsVisible = true;
            LoginButton.IsVisible = true;

        }

        private void SelectRegisterButton_Clicked(object sender, EventArgs e)
        {
            SelectLoginButton.IsVisible = false;
            SelectRegisterButton.IsVisible = false;
            UsernameEntry.IsVisible = true;
            PasswordEntry.IsVisible = true;
            PasswordAgainEntry.IsVisible = true;
            BackToSelectButton.IsVisible = true;
            RegisterButton.IsVisible = true;
        }

        private void BackToSelectButton_Clicked(object sender, EventArgs e)
        {
            SelectLoginButton.IsVisible = true;
            SelectRegisterButton.IsVisible = true;
            UsernameEntry.IsVisible = false;
            PasswordEntry.IsVisible = false;
            PasswordAgainEntry.IsVisible = false;
            BackToSelectButton.IsVisible = false;
            RegisterButton.IsVisible = false;
            LoginButton.IsVisible = false;
        }
    }
}