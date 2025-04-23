using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidX.AppCompat.App;


namespace RushHour_project
{
    [Activity(Label = "SplashActivity", Theme = "@style/AppTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_splash);

            // Create your application here
            Task.Delay(2000).ContinueWith(_ =>
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}