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

namespace RushHour_project.Adapters
{
    public class LeaderboardAdapter : BaseAdapter<UserRecords>
    {
        private readonly Context context;
        private readonly List<UserRecords> users;
        private int expandedPosition = -1; // Keeps track of which item is expanded

        public LeaderboardAdapter(Context context, List<UserRecords> users)
        {
            this.context = context;
            this.users = users;
        }

        public override UserRecords this[int position] => users[position];
        public override int Count => users.Count;
        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = LayoutInflater.From(context);
            var view = convertView ?? inflater.Inflate(Resource.Layout.leaderboard_item, parent, false);

            var usernameTextView = view.FindViewById<TextView>(Resource.Id.usernameTextView);
            var rankTextView = view.FindViewById<TextView>(Resource.Id.rankTextView);
            var expandedLayout = view.FindViewById<LinearLayout>(Resource.Id.expandedLayout);
            var recordsTextView = view.FindViewById<TextView>(Resource.Id.recordsTextView);
            var pointsTextView = view.FindViewById<TextView>(Resource.Id.pointsTextView);
            var lbEmailTxt = view.FindViewById<TextView>(Resource.Id.lbEmailTxt);

            var user = users[position];

            pointsTextView.Text = user.Points.ToString();
            usernameTextView.Text = user.Username;
            lbEmailTxt.Text = user.Email;
            rankTextView.Text = $"{position + 1}.";

            //setting colours to the textviews
            usernameTextView.SetTextColor(Android.Graphics.Color.ParseColor("#ffffff"));
            lbEmailTxt.SetTextColor(Android.Graphics.Color.ParseColor("#ffffff"));
            rankTextView.SetTextColor(Android.Graphics.Color.ParseColor("#f4c33a"));
            pointsTextView.SetTextColor(Android.Graphics.Color.ParseColor("#e14326"));


            // Build the records string
            var builder = new StringBuilder();
            foreach (var record in user.Records.OrderBy(kvp => kvp.Key))
            {
                if (record.Value != null)
                {
                    builder.AppendLine($"Level {record.Key}: {record.Value} steps");
                }
                else
                {
                    builder.AppendLine($"Level {record.Key}: not completed");

                }
            }
            recordsTextView.Text = builder.ToString();
            recordsTextView.SetTextColor(Android.Graphics.Color.ParseColor("#f4c33a"));


            // Toggle expanded view
            expandedLayout.Visibility = (position == expandedPosition) ? ViewStates.Visible : ViewStates.Gone;

            view.Click -= OnItemClick; // Prevent duplicate handlers
            view.Click += OnItemClick;

            void OnItemClick(object sender, EventArgs e)
            {
                if (expandedPosition == position)
                    expandedPosition = -1; // Collapse
                else
                    expandedPosition = position; // Expand selected

                NotifyDataSetChanged();
            }

            return view;
        }
    }
}