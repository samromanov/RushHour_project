using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace RushHour_project
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        private Button _playBtn_home, _statsBtn_home, _settingsBtn_home, _loginBtn, _leaderboardBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Init();
        }
        void Init()
        {
            _playBtn_home = FindViewById<Button>(Resource.Id.playBtn_home);
            _statsBtn_home = FindViewById<Button>(Resource.Id.statsBtn_home);
            _settingsBtn_home = FindViewById<Button>(Resource.Id.settingsBtn_home);
            _loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
            _leaderboardBtn = FindViewById<Button>(Resource.Id.leaderboardBtn);

            _playBtn_home.Click += _playBtn_home_Click;
            _loginBtn.Click += _loginBtn_Click;
        }

        private void _loginBtn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AuthenticationPageActivity));
            StartActivity(intent);
        }

        private void _playBtn_home_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LevelsPageActivity));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}