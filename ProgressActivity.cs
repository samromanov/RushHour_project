using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RushHour_project
{
    [Activity(Label = "ProgressActivity")]
    public class ProgressActivity : Activity
    {
        public ImageButton _progress_backBtn;
        public TextView _progress_points, _progress_levelsCompleted, _progress_levelsStatus;
        public bool isLoggedIn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_progress);
            Init();
        }
        public void Init()
        {
            _progress_backBtn = FindViewById<ImageButton>(Resource.Id.progress_backBtn);
            _progress_points = FindViewById<TextView>(Resource.Id.progress_points);
            _progress_levelsCompleted = FindViewById<TextView>(Resource.Id.progress_levelsCompleted);
            _progress_levelsStatus = FindViewById<TextView>(Resource.Id.progress_levelsStatus);

            _progress_backBtn.Click += _progress_backBtn_Click;

            //sharedpreference / set userRecords
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);
            isLoggedIn = prefs.GetBoolean("isLoggedIn", false);
            string userRecords_json;
            Dictionary<int, int?> userRecords = new Dictionary<int, int?>();

            if (isLoggedIn == true)
                userRecords_json = prefs.GetString("userRecords", null);
            else
                userRecords_json = prefs.GetString("localUserRecords", null);

            if (userRecords_json != null)
                userRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(userRecords_json);

            // Build the records string
            var builder = new StringBuilder();
            int levelsCompleted = 0;
            int totalSteps = 0;
            foreach (var record in userRecords.OrderBy(kvp => kvp.Key))
            {
                if (record.Value != null)
                {
                    builder.AppendLine($"Level {record.Key}: {record.Value} steps");
                    levelsCompleted += 1;
                    totalSteps += (int)record.Value;
                }
                else
                {
                    builder.AppendLine($"Level {record.Key}: not completed");

                }
            }
            int totalPoints = (levelsCompleted * 1000) - totalSteps;
            _progress_levelsStatus.Text = builder.ToString();
            _progress_levelsCompleted.Text = levelsCompleted.ToString();
            _progress_points.Text = totalPoints.ToString();
        }

        private void _progress_backBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}