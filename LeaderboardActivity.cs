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
    [Activity(Label = "LeaderboardActivity")]
    public class LeaderboardActivity : Activity
    {
        public List<UserRecords> leaderboardList = new List<UserRecords>();
        public ImageButton _leaderboard_backBtn;
        public ListView _leaderboardListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_leaderboard);
            Init();
        }
        public async void Init()
        {
            _leaderboard_backBtn = FindViewById<ImageButton>(Resource.Id.leaderboard_backBtn);
            _leaderboardListView = FindViewById<ListView>(Resource.Id.leaderboardListView);


            User user = new User();
            leaderboardList = await user.GetSortedLeaderboard();

            var adapter = new LeaderboardAdapter(this, leaderboardList);
            _leaderboardListView.Adapter = adapter;

            _leaderboard_backBtn.Click += _leaderboard_backBtn_Click;
        }

        private void _leaderboard_backBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}