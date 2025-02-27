﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using AndroidX.AppCompat.App;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
using Xamarin.Essentials;

namespace HomeAutomationServerApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTask,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : AppCompatActivity
    {
        WebView _webView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            AppCenter.Start("081769ec-df8f-4452-9118-ecb011ae65ec", typeof(Distribute), typeof(Crashes), typeof(Analytics));

            JSBridge.Init(this.ApplicationContext);


            //if (DeviceInfo.Version.Major > 10)
            //{
            //    this.Window.SetDecorFitsSystemWindows(false);
            //    this.Window.InsetsController.Hide(WindowInsets.Type.SystemBars());
            //}
            //else
            //{
                int uiOptions = (int)Window.DecorView.SystemUiVisibility;

                uiOptions |= (int)SystemUiFlags.LowProfile;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                uiOptions |= (int)SystemUiFlags.HideNavigation;
                uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            //}

            this.Window.AddFlags(WindowManagerFlags.Fullscreen | WindowManagerFlags.TurnScreenOn | WindowManagerFlags.LayoutNoLimits);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _webView = FindViewById<WebView>(Resource.Id.webView1);
            _webView.Settings.JavaScriptEnabled = true;
            _webView.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            _webView.Settings.DomStorageEnabled = true;

            _webView.SetWebViewClient(new AppWebViewClient());
            _webView.AddJavascriptInterface(new JSBridge(), "manoirDeviceApp");
            _webView.LoadUrl(JSBridge.GetApplication());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}