using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RushHour_project
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        private Button _playBtn_home, _loginProfileBtn, _settingsBtn, _progressBtn, _leaderboardBtn;
        private const int LOGIN_REQUEST_CODE = 1; // code for Intent login

        //Authentication
        private bool isLoggedIn = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SupportActionBar?.Hide(); //hiding the action bar in the top

            Init();
        }
        void Init()
        {
            _playBtn_home = FindViewById<Button>(Resource.Id.playBtn_home);
            _loginProfileBtn = FindViewById<Button>(Resource.Id.loginProfileBtn);
            _settingsBtn = FindViewById<Button>(Resource.Id.settingsBtn);
            _progressBtn = FindViewById<Button>(Resource.Id.progressBtn);
            _leaderboardBtn = FindViewById<Button>(Resource.Id.leaderboardBtn);

            _playBtn_home.Click += PlayBtn_Click;
            _loginProfileBtn.Click += LoginProfileBtn_Click;
            _settingsBtn.Click += SettingsBtn_Click;
            _progressBtn.Click += _progressBtn_Click;
            _leaderboardBtn.Click += _leaderboardBtn_Click;


            
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);
            isLoggedIn = prefs.GetBoolean("isLoggedIn", false);

            if (isLoggedIn == false) // if not logged in
            {
                _loginProfileBtn.Text = "Login";
                _leaderboardBtn.Visibility = Android.Views.ViewStates.Gone;

            }
            else //if logged in
            {
                _loginProfileBtn.Text = "Profile";
                _leaderboardBtn.Visibility = Android.Views.ViewStates.Visible;
            }

        }

        private void _leaderboardBtn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LeaderboardActivity));
            StartActivity(intent);
        }

        private void _progressBtn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ProgressActivity));
            StartActivity(intent);
        }

        private void SettingsBtn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }

        private void LoginProfileBtn_Click(object sender, System.EventArgs e)
        {
            if (isLoggedIn == false) 
            {
                Intent intent = new Intent(this, typeof(AuthenticationPageActivity));
                StartActivityForResult(intent, LOGIN_REQUEST_CODE);
            }
            else // if logged in
            {
                Intent intent = new Intent(this, typeof(ProfileActivity));
                StartActivity(intent);
            }
        }

        private void PlayBtn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LevelSelectionActivity));
            StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == LOGIN_REQUEST_CODE)
            {
                if (resultCode == Result.Ok)
                {
                    isLoggedIn = true;
                    _loginProfileBtn.Text = "Profile";
                }
                else if (resultCode == Result.Canceled)
                {
                    isLoggedIn = false;
                    _loginProfileBtn.Text = "Login";
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
