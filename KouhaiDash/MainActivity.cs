using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

namespace KouhaiDash
{
    [Activity(Label = "KouhaiDash!", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        WebView myWebView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            myWebView = FindViewById<WebView>(Resource.Id.MainWebView);
            myWebView.Settings.JavaScriptEnabled = true;
            myWebView.Settings.UseWideViewPort = true;
            myWebView.Settings.StandardFontFamily = myWebView.Settings.SansSerifFontFamily;
            myWebView.LoadUrl("http://kouhaidash.herokuapp.com");
            myWebView.SetWebViewClient(new KouhaiWebViewClient());
        }

        public class KouhaiWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return true;
            }
        }

        public override bool OnKeyDown(Android.Views.Keycode keyCode, Android.Views.KeyEvent e)
        {
            if (keyCode == Keycode.Back && myWebView.CanGoBack())
            {
                myWebView.GoBack();
                return true;
            }

            return base.OnKeyDown(keyCode, e);
        }
    }
}

