using MobileBooking.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        ListView listView;
        public ListView ListView { get { return listView; } }

        public MenuPage()
        {
            var flyoutPageItems = new List<FlyoutPageItem>();
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "Home",
                TargetType = typeof(HomePage)
            });
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "Video Call",
                TargetType = typeof(VideoPage)
            });
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "Logout",
                TargetType = typeof(LoginPage)
            });

            listView = new ListView
            {
                ItemsSource = flyoutPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(label, 0, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            IconImageSource = "hamburger.png";
            Title = "Menu";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { listView }
            };

        }

    }
}