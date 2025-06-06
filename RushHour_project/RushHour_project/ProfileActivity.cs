using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RushHour_project.Sounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RushHour_project
{
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {
        public Button _logOutBtn; // log out button
        public ImageButton _profile_backBtn, _menuButton; // back button
        public TextView _profile_username, _profile_email, _profile_date; // user properties
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_profile);
            Init();
        }
        void Init()
        {
            _logOutBtn = FindViewById<Button>(Resource.Id.logOutBtn);
            _profile_backBtn = FindViewById<ImageButton>(Resource.Id.profile_backBtn);
            _profile_username = FindViewById<TextView>(Resource.Id.profile_username);
            _profile_email = FindViewById<TextView>(Resource.Id.profile_email);
            _profile_date = FindViewById<TextView>(Resource.Id.profile_date);
            _menuButton = FindViewById<ImageButton>(Resource.Id.profileMenuButton);

            _menuButton.Click += (s, e) =>
            {
                PopupMenu popup = new PopupMenu(this, _menuButton);
                popup.MenuInflater.Inflate(Resource.Menu.menu_profile, popup.Menu);
                popup.MenuItemClick += (sender, args) =>
                {
                    switch (args.Item.ItemId)
                    {
                        case Resource.Id.menu_option1:
                            // Handle Option 1
                            break;
                        case Resource.Id.menu_option2:
                            // Handle Option 2
                            break;
                        case Resource.Id.menu_option3:
                            // Handle Option 3
                            break;
                    }
                };
                popup.Show();
            };


            // button click implementation
            _profile_backBtn.Click += BackBtn_Click;
            _logOutBtn.Click += LogOutBtn_Click;

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);

            _profile_username.Text = prefs.GetString("username", "user");
            _profile_email.Text = prefs.GetString("email", "");
            _profile_date.Text = prefs.GetString("regDate", "not found");

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_profile, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_option1:
                    // Handle Option 1
                    return true;
                case Resource.Id.menu_option2:
                    // Handle Option 2
                    return true;
                case Resource.Id.menu_option3:
                    // Handle Option 3
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        private async void LogOutBtn_Click(object sender, EventArgs e)
        {
            SoundEffectManager.Init(this);
            SoundEffectManager.Play("click");
            User user = new User(_profile_email.Text);
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

        private void BackBtn_Click(object sender, EventArgs e)
        {
            SoundEffectManager.Init(this);
            SoundEffectManager.Play("click");
            Finish();
        }
    }
}