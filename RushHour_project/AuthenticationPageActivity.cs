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

namespace RushHour_project
{
    [Activity(Label = "AuthenticationPageActivity")]
    public class AuthenticationPageActivity : Activity
    {
        //layouts
        private LinearLayout _login_logo_layout, _fields_layout;
        private ImageView _auth_gameLogo;

        //authentication fields
        private EditText _auth_username, _auth_email, _auth_password;

        //buttons
        private ImageButton _auth_backBtn;
        private Button _loginSignupBtn, _switchAuthenticationMethodBtn;
        private Button _signup_login; //it changes depending if the page is login/register

        //textview
        private TextView _auth_question; // eg. Don't have account?

        //to check if login/register
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
            _auth_question = FindViewById<TextView>(Resource.Id.auth_question);
            _auth_backBtn = FindViewById<ImageButton>(Resource.Id.auth_backBtn);
            _login_logo_layout = FindViewById<LinearLayout>(Resource.Id.login_logo_layout); // the layout where the logo is placed
            _fields_layout = FindViewById<LinearLayout>(Resource.Id.fields_layout); //the layout where all of the edittext fields
            _auth_gameLogo = FindViewById<ImageView>(Resource.Id.auth_gameLogo);
            _auth_username = FindViewById<EditText>(Resource.Id.auth_username);
            _auth_email = FindViewById<EditText>(Resource.Id.auth_email);
            _auth_password = FindViewById<EditText>(Resource.Id.auth_password);
            _loginSignupBtn = FindViewById<Button>(Resource.Id.loginRegisterBtn);
            _switchAuthenticationMethodBtn = FindViewById<Button>(Resource.Id.signup_login);

            _auth_username.Visibility = ViewStates.Gone;
            _loginSignupBtn.Text = "Login";
            _loginSignupBtn.SetBackgroundColor(Android.Graphics.Color.ParseColor("#ff669900")); // Light green
            _auth_question.Text = "Don't have account?";
            _switchAuthenticationMethodBtn.Text = "Sign up";

            //handle button clicks
            _switchAuthenticationMethodBtn.Click += SwitchAuthenticationMethod_Click;
            _loginSignupBtn.Click += LoginSignupBtn_Click;
            _auth_backBtn.Click += _auth_backBtn_Click;
        }

        private void _auth_backBtn_Click(object sender, EventArgs e)
        {
            SetResult(Result.Canceled);
            Finish();
        }

        private async void LoginSignupBtn_Click(object sender, EventArgs e)
        {
            if (_auth_email.Text == "" || _auth_password.Text == "")
            {
                Toast.MakeText(this, "Empty fields detected!", ToastLength.Short).Show();
            }
            else if (!_auth_email.Text.Contains("@"))
            {
                Toast.MakeText(this, "An email must contain (@)", ToastLength.Short).Show();
            }
            else if (_auth_password.Text.Length < 8)
            {
                Toast.MakeText(this, "A password must contain at least 8 characters", ToastLength.Short).Show();
            }
            else
            {
                switch (isLogin)
                {
                    case true: // in case this page is a login page
                        try
                        {
                            User user = new User(_auth_email.Text, _auth_password.Text);
                            if (await user.Login() == true)
                            {                              
                                Intent homeReturnIntent = new Intent(this,typeof(MainActivity));
                                SetResult(Result.Ok, homeReturnIntent);
                                homeReturnIntent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                                StartActivity(homeReturnIntent);
                                Finish();
                            }
                            else
                            {
                                Toast.MakeText(this, "Wrong email or password", ToastLength.Short).Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Short).Show();
                        }
                        break;
                    case false: // in case the page is sign up page
                        if (_auth_username.Text == "")
                        {
                            Toast.MakeText(this, "Empty fields detected!", ToastLength.Short).Show();
                        }
                        else
                        {
                            try
                            {
                                User user = new User(_auth_username.Text, _auth_email.Text, _auth_password.Text);
                                if (await user.Register() == true)
                                {
                                    Intent homeReturnIntent = new Intent(this, typeof(MainActivity));
                                    SetResult(Result.Ok, homeReturnIntent);
                                    homeReturnIntent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                                    StartActivity(homeReturnIntent);
                                    Finish();
                                }
                                else
                                {
                                    Toast.MakeText(this, "Email has already been taken", ToastLength.Short).Show();
                                }
                            }
                            catch (Exception ex)
                            {
                                Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Short).Show();
                            }
                            
                        }
                        break;
                }
            }
        }

        private void SwitchAuthenticationMethod_Click(object sender, EventArgs e)
        {
            switch (isLogin)
            {
                case true: // ---in case it's a login page---
                    Task.Delay(1000).ContinueWith(_ =>
                    {
                        _auth_username.Visibility = ViewStates.Visible;
                        _loginSignupBtn.Text = "Sign up";
                        _loginSignupBtn.SetBackgroundColor(Android.Graphics.Color.ParseColor("#f4c33a")); // Light orange
                        _auth_question.Text = "Have account?";
                        _switchAuthenticationMethodBtn.Text = "Log in";




                        isLogin = false;
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                    break;
                case false:
                    Task.Delay(1000).ContinueWith(_ =>
                    {
                        _auth_username.Visibility = ViewStates.Gone;
                        _loginSignupBtn.Text = "Login";
                        _loginSignupBtn.SetBackgroundColor(Android.Graphics.Color.ParseColor("#ff669900")); // Light green
                        _auth_question.Text = "Don't have account?";
                        _switchAuthenticationMethodBtn.Text = "Sign up";




                        isLogin = true;
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                    break;
            }
        }
    }
}