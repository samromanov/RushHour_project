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
using RushHour_project.Sounds;
using Firebase.Firestore;

namespace RushHour_project
{
    [Activity(Label = "LeaderboardActivity")]
    public class LeaderboardActivity : Activity
    {
        private IListenerRegistration leaderboardListenerRegistration;
        public List<UserRecords> leaderboardList = new List<UserRecords>();
        public ImageButton _leaderboard_backBtn;
        public ListView _leaderboardListView;
        public LeaderboardAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_leaderboard);
            Init();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            leaderboardListenerRegistration?.Remove();
        }
        public async void Init()
        {
            _leaderboard_backBtn = FindViewById<ImageButton>(Resource.Id.leaderboard_backBtn);
            _leaderboardListView = FindViewById<ListView>(Resource.Id.leaderboardListView);

            _adapter = new LeaderboardAdapter(this, leaderboardList);
            _leaderboardListView.Adapter = _adapter;

            User user = new User();
            leaderboardList = await user.GetSortedLeaderboard();

            leaderboardListenerRegistration= user.ListenToLeaderboardUpdates((updatedList) =>
            {
                RunOnUiThread(() =>
                {
                    _adapter.UpdateData(updatedList); // this method in the adapter
                    _adapter.NotifyDataSetChanged();
                });
            });

            _leaderboard_backBtn.Click += _leaderboard_backBtn_Click;
        }

        private void _leaderboard_backBtn_Click(object sender, EventArgs e)
        {
            SoundEffectManager.Play("click");
            Finish();
        }


    }
}