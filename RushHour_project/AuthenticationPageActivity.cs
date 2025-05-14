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
        private LinearLayout _login_logo_layout, _fields_layout;
        private EditText _auth_email, _auth_password;
        private Button _loginSignupBtn, _switchAuthenticationMethod;
        private bool isLogin = true; // login in default
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.authentication_page);
            Init();
        }
        void Init()
        {
            _login_logo_layout = FindViewById<LinearLayout>(Resource.Id.login_logo_layout); // the layout where the logo is placed
            _fields_layout = FindViewById<LinearLayout>(Resource.Id.fields_layout); //the layout where all of the edittext fields
            _auth_email = FindViewById<EditText>(Resource.Id.auth_email);
            _auth_password = FindViewById<EditText>(Resource.Id.auth_password);
            _loginSignupBtn = FindViewById<Button>(Resource.Id.loginRegisterBtn);
            _switchAuthenticationMethod = FindViewById<Button>(Resource.Id.signup_login);


            _switchAuthenticationMethod.Click += SwitchAuthenticationMethod_Click;
            _loginSignupBtn.Click += LoginSignupBtn_Click;
        }

        private void LoginSignupBtn_Click(object sender, EventArgs e)
        {
            if (isLogin) // if the meh=thod is to login
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

        private void SwitchAuthenticationMethod_Click(object sender, EventArgs e)
        {
            if (isLogin) // clicking while being in the login page
            {
                _auth_username.Visibility = ViewStates.Visible;
                _loginSignupBtn.Text = "Sign up";
                _toRegisterBtn.Text = "Have an account? Log in here!";
                isLogin = false;
            }
            else // if isLogin == false
            {
                _auth_username.Visibility = ViewStates.Gone;
                _loginSignupBtn.Text = "Login";
                _toRegisterBtn.Text = "Don't have an account? Sign up here!";
                isLogin = true;
            }
        }
    }
}