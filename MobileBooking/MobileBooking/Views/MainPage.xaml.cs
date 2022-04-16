using MobileBooking.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public static MainPage CurrentMainPage;

        public MainPage()
        {
            InitializeComponent();
            CurrentMainPage = this;
            FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
            flyoutPage.ListView.ItemSelected += OnItemSelected;

            //MasterBehavior = MasterBehavior.Popover;

            //MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }



        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                flyoutPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;
                    case (int)MenuItemType.VideoCalls:
                        MenuPages.Add(id, new NavigationPage(new VideoPage()));
                        break;
                    //case (int)MenuItemType.MyReservations:
                    //    MenuPages.Add(id, new NavigationPage(new HomePage()));
                    //    break;
                    //case (int)MenuItemType.Stories:
                    //    MenuPages.Add(id, new NavigationPage(new AboutPage()));
                    //    break;
                    case (int)MenuItemType.Logout:
                        await Navigation.PopModalAsync();
                        return;
                    default:
                        await DisplayAlert("Error", "Stranka neexistuje", "OK");
                        return;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                //if (Device.RuntimePlatform == Device.Android)
                //    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}