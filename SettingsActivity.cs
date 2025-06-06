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
            var prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);

            _username = prefs.GetString("username", null); // null is the default if not found
            _email = prefs.GetString("email", null);
            _password = prefs.GetString("password", null);

        }

        
    }
}