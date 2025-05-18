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
using RushHour_project.Classes;
using RushHour_project.Adapters;

namespace RushHour_project
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        public Button _logoutBtn;
        public string _email, _password, _username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_settings);
            Init();
        }
        void Init()
        {
            _logoutBtn = FindViewById<Button>(Resource.Id.logoutBtn);

            var prefs = Application.Context.GetSharedPreferences("CURRENT_USER_FILE", FileCreationMode.Private);

            _username = prefs.GetString("username", null); // null is the default if not found
            _email = prefs.GetString("email", null);
            _password = prefs.GetString("password", null);

            _logoutBtn.Click += _logoutBtn_Click;
        }

        private async void _logoutBtn_Click(object sender, EventArgs e)
        {
            User user = new User(_email);
            try
            {
                if (await user.Logout() == true)
                {
                    Toast.MakeText(this, "Logout successfully", ToastLength.Short).Show();
                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                    StartActivity(intent);
                    Finish(); // finish the current activity
                }
                else
                {
                    Toast.MakeText(this, "Logout failed", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Short).Show();
            }
        }
    }
}