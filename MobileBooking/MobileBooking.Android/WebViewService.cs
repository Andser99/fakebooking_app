using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileBooking;
using MobileBooking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(MobileBooking.Droid.WebViewService))]
namespace MobileBooking.Droid
{
    public class WebViewService : IWebViewService
    {
        public string GetContent()
        {
            return "file:///android_asset/call.html";
        }
    }
}