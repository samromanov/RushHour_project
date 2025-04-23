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

namespace RushHour_project
{
    [Activity(Label = "AuthenticationPageActivity")]
    public class AuthenticationPageActivity : Activity
    {
        private EditText _auth_username, _auth_email, _auth_password;
        private Button _loginRegisterBtn, _toRegisterBtn;
        private bool isLogin = true; // false if register
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.authentication_page);
            Init();
        }
        void Init()
        {
            _auth_username = FindViewById<EditText>(Resource.Id.auth_username);
            _auth_email = FindViewById<EditText>(Resource.Id.auth_email);
            _auth_password = FindViewById<EditText>(Resource.Id.auth_password);
            _loginRegisterBtn = FindViewById<Button>(Resource.Id.loginRegisterBtn);
            _toRegisterBtn = FindViewById<Button>(Resource.Id.toRegisterBtn);

            _auth_username.Visibility = ViewStates.Gone;

            _toRegisterBtn.Click += _toRegisterBtn_Click;
            _loginRegisterBtn.Click += _loginRegisterBtn_Click;
        }

        private void _loginRegisterBtn_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                if (!_auth_email.Text.Contains("@"))
                {
                    Toast.MakeText(this, "An email must contain (@)", ToastLength.Short).Show();
                }
                else if (_auth_password.Text.Length < 8)
                {
                    Toast.MakeText(this, "A password must contain at least 8 characters", ToastLength.Short).Show();
                }
                else
                {
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
            }
            else
            {
                if (!_auth_email.Text.Contains("@"))
                {
                    Toast.MakeText(this, "An email must contain (@)", ToastLength.Short).Show();
                }
                else if (_auth_password.Text.Length < 8)
                {
                    Toast.MakeText(this, "A password must contain at least 8 characters", ToastLength.Short).Show();
                }
                else
                {
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
            }
        }

        private void _toRegisterBtn_Click(object sender, EventArgs e)
        {
            if (isLogin) // clicking while being in the login page
            {
                _auth_username.Visibility = ViewStates.Visible;
                _loginRegisterBtn.Text = "Sign up";
                _toRegisterBtn.Text = "Have an account? Log in here!";
                isLogin = false;
            }
            else // if isLogin == false
            {
                _auth_username.Visibility = ViewStates.Gone;
                _loginRegisterBtn.Text = "Login";
                _toRegisterBtn.Text = "Don't have an account? Sign up here!";
                isLogin = true;
            }
        }
    }
}