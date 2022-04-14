using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Webkit;
using MobileBooking.Droid;
using MobileBooking.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(GenericWebView), typeof(AndroidWebViewRenderer))]

namespace MobileBooking.Droid
{
    public class AndroidWebViewRenderer : WebViewRenderer
    {
        Activity mContext;
        public AndroidWebViewRenderer(Context context) : base(context)
        {
            global::Android.Webkit.WebView.SetWebContentsDebuggingEnabled(true);
            this.mContext = context as Activity;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            Control.Settings.JavaScriptEnabled = true;
            Control.ClearCache(true);
            Control.Settings.MediaPlaybackRequiresUserGesture = false;
            Control.Settings.DomStorageEnabled = true;
            Control.Settings.SetRenderPriority(WebSettings.RenderPriority.High);
            Control.Settings.SetPluginState(WebSettings.PluginState.OnDemand);
            Control.SetWebChromeClient(new MyWebClient(mContext));
            SetLayerType(Android.Views.LayerType.Hardware, null);
            LoadUrl("https://fakebooking.herokuapp.com/index.html");
        }
        public class MyWebClient : WebChromeClient
        {
            Activity mContext;
            public MyWebClient(Activity context)
            {
                this.mContext = context;
            }
            [TargetApi(Value = 21)]
            public override void OnPermissionRequest(PermissionRequest request)
            {
                mContext.RunOnUiThread(() =>
                {
                    request.Grant(request.GetResources());
                });
            }
        }
    }
}