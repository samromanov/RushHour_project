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
using RushHour_project.Sounds;

namespace RushHour_project
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SoundEffectManager.OnLoaded = () =>
            {
                SoundEffectManager.Play("loading");
            };
            SoundEffectManager.Init(this);

            SetContentView(Resource.Layout.activity_splash);

            if (MyApp.HasShownSplash)
            {
                // Skip splash, go directly to MainActivity
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
                return;
            }

            MyApp.HasShownSplash = true;         

            // Delay and then start MainActivity
            Task.Delay(2000).ContinueWith(_ =>
            {
                RunOnUiThread(() =>
                {
                    StartActivity(new Intent(this, typeof(MainActivity)));
                    Finish();
                });
            });
        }
    }
}
