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
        public ImageButton _profile_backBtn, _menuButton; // back button
        public LinearLayout _editProfileLayout, _profileLayout;
        public EditText _edit_email, _edit_username;
        public Button _editProfileCancelBtn, _editProfileSaveBtn;
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
            _profile_backBtn = FindViewById<ImageButton>(Resource.Id.profile_backBtn);
            _profile_username = FindViewById<TextView>(Resource.Id.profile_username);
            _profile_email = FindViewById<TextView>(Resource.Id.profile_email);
            _profile_date = FindViewById<TextView>(Resource.Id.profile_date);
            _menuButton = FindViewById<ImageButton>(Resource.Id.profileMenuButton);
            _editProfileLayout = FindViewById<LinearLayout>(Resource.Id.editProfileLayout);
            _profileLayout = FindViewById<LinearLayout>(Resource.Id.profileLayout);
            _edit_username = FindViewById<EditText>(Resource.Id.edit_username);
            _edit_email = FindViewById<EditText>(Resource.Id.edit_email);
            _editProfileCancelBtn = FindViewById<Button>(Resource.Id.editProfileCancelBtn);
            _editProfileSaveBtn = FindViewById<Button>(Resource.Id.editProfileSaveBtn);

            _menuButton.Click += (s, e) =>
            {
                PopupMenu popup = new PopupMenu(this, _menuButton);
                popup.MenuInflater.Inflate(Resource.Menu.menu_profile, popup.Menu);
                popup.MenuItemClick += async (sender, args) =>
                {
                    switch (args.Item.ItemId)
                    {
                        case Resource.Id.menu_logOut:
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
                            break;
                        case Resource.Id.menu_edit:
                            _editProfileLayout.Visibility = ViewStates.Visible;
                            _profileLayout.Visibility = ViewStates.Gone;
                            _profile_backBtn.Enabled = false;
                            _menuButton.Enabled = false;
                            _edit_email.Text = _profile_email.Text;
                            _edit_username.Text = _profile_username.Text;
                            break;
                        case Resource.Id.menu_reset:
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Reset Account");
                            alert.SetMessage("Are you sure you want to reset your account? All of your game data and achievements will be permanently lost!");
                            alert.SetPositiveButton("Proceed", async (senderAlert, args) =>
                            {
                                SoundEffectManager.Init(this);
                                SoundEffectManager.Play("click");
                                User user = new User();
                                bool success = await user.ResetUserData();
                                if (success)
                                {
                                    Toast.MakeText(this, "Account reset successfully!", ToastLength.Short).Show();
                                    // Update displayed profile info to reflect reset state
                                    ISharedPreferences prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);
                                    _profile_username.Text = prefs.GetString("username", "user");
                                    _profile_email.Text = prefs.GetString("email", "");
                                    _profile_date.Text = prefs.GetString("regDate", "not found");
                                }
                                else
                                {
                                    Toast.MakeText(this, "Failed to reset account. Please try again.", ToastLength.Short).Show();
                                }
                            });
                            alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                            {
                                SoundEffectManager.Init(this);
                                SoundEffectManager.Play("click");
                                alert.Dispose();
                            });
                            Dialog dialog = alert.Create();
                            dialog.Show();
                            break;
                        case Resource.Id.menu_delete:
                            AlertDialog.Builder deleteAlert = new AlertDialog.Builder(this);
                            deleteAlert.SetTitle("Delete Account");
                            deleteAlert.SetMessage("Are you sure you want to permanently delete your account? This action cannot be undone.");
                            deleteAlert.SetPositiveButton("Proceed", async (senderDeleteAlert, args) =>
                            {
                                SoundEffectManager.Init(this);
                                SoundEffectManager.Play("click");
                                User userToDelete = new User();
                                bool success = await userToDelete.DeleteAccount();
                                if (success)
                                {
                                    // After successful deletion, navigate to MainActivity (login screen)
                                    Intent intent = new Intent(this, typeof(MainActivity));
                                    intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                                    StartActivity(intent);
                                    Finish(); // finish the current activity
                                }
                                else
                                {
                                    Toast.MakeText(this, "Failed to delete account. Please try again.", ToastLength.Short).Show();
                                }
                            });
                            deleteAlert.SetNegativeButton("Cancel", (senderDeleteAlert, args) =>
                            {
                                SoundEffectManager.Init(this);
                                SoundEffectManager.Play("click");
                                deleteAlert.Dispose();
                            });
                            Dialog deleteDialog = deleteAlert.Create();
                            deleteDialog.Show();
                            break;
                    }
                };
                popup.Show();
            };


            // button click implementation
            _profile_backBtn.Click += BackBtn_Click;
            _editProfileCancelBtn.Click += _editProfileCancelBtn_Click;
            _editProfileSaveBtn.Click += _editProfileSaveBtn_Click;

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);

            _profile_username.Text = prefs.GetString("username", "user");
            _profile_email.Text = prefs.GetString("email", "");
            _profile_date.Text = prefs.GetString("regDate", "not found");

        }

        private async void _editProfileSaveBtn_Click(object sender, EventArgs e)
        {
            string newUsername = _edit_username.Text;
            string newEmail = _edit_email.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newEmail))
            {
                Toast.MakeText(this, "Username and Email cannot be empty.", ToastLength.Short).Show();
                return;
            }
            if (!newEmail.Contains("@"))
            {
                Toast.MakeText(this, "An email must contain [@] sign", ToastLength.Short).Show();
                return;
            }

            User user = new User(); // Create a user instance to call the update method
            bool success = await user.UpdateProfile(newUsername, newEmail);

            if (success)
            {
                // Update the displayed profile information
                _profile_username.Text = newUsername;
                _profile_email.Text = newEmail;
                Toast.MakeText(this, "Profile updated successfully!", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Failed to update profile. Please try again.", ToastLength.Short).Show();
            }

            // After save (or failure), revert UI
            _editProfileLayout.Visibility = ViewStates.Gone;
            _profileLayout.Visibility = ViewStates.Visible;
            _profile_backBtn.Enabled = true;
            _menuButton.Enabled = true;
        }

        private void _editProfileCancelBtn_Click(object sender, EventArgs e)
        {
            _editProfileLayout.Visibility = ViewStates.Gone;
            _profileLayout.Visibility = ViewStates.Visible;
            _profile_backBtn.Enabled = true;
            _menuButton.Enabled = true;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            SoundEffectManager.Init(this);
            SoundEffectManager.Play("click");
            Finish();
        }
    }
}