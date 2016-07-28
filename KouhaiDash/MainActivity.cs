using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

using System.Threading;

namespace KouhaiDash
{
    [Activity(Label = "KouhaiDash!", MainLauncher = true, 
        Icon = "@drawable/icon", Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class MainActivity : Activity
    {
        WebView myWebView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var progressDialog = ProgressDialog.Show(this, "Connecting to KouhaiServer", 
                "Isn't it a beautiful day today?", true);

            new Thread(new ThreadStart(() => {
                Thread.Sleep(4 * 1000);
                this.RunOnUiThread(() => {
                    progressDialog.Dismiss();
                });
            })).Start();

            myWebView = FindViewById<WebView>(Resource.Id.MainWebView);
            myWebView.Settings.JavaScriptEnabled = true;
            myWebView.Settings.UseWideViewPort = true;
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

