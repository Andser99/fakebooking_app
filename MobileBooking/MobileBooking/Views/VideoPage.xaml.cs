using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WebRTCme;
using MobileBooking.Services;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        public VideoPage()
        {
            InitializeComponent();

            StackLayout contentLayout = new StackLayout();
            Label l = new Label() { Text = "asdf" };
            contentLayout.Children.Add(l);

            GenericWebView webView = new GenericWebView();
            HtmlWebViewSource src = new HtmlWebViewSource();
            src.BaseUrl = "https://fakebooking.herokuapp.com/index.html";
            //src.BaseUrl = DependencyService.Get<IWebViewService>().GetContent();
            //src.BaseUrl = "https://fakebooking.herokuapp.com/index.html";
            //webView.HorizontalOptions = LayoutOptions.FillAndExpand;
            //webView.VerticalOptions = LayoutOptions.FillAndExpand;
            webView.HeightRequest = 1000;
            webView.WidthRequest = 1000;
            webView.Source = src;
            webView.HeightRequest = 1000;
            webView.WidthRequest = 1000;

            contentLayout.Children.Add(webView);
            webView.HeightRequest = 1000;
            webView.WidthRequest = 1000;


            Content = contentLayout;

            //LocalStream = await _mediaStreamService.GetCameraMediaStreamAsync();
        }
    }
}