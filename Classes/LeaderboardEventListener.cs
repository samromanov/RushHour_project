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
using Firebase.Firestore;
using Newtonsoft.Json;

namespace RushHour_project.Classes
{
    public class LeaderboardEventListener : Java.Lang.Object, IEventListener
    {
        private readonly Action<List<UserRecords>> _onLeaderboardChanged;

        public LeaderboardEventListener(Action<List<UserRecords>> onLeaderboardChanged)
        {
            _onLeaderboardChanged = onLeaderboardChanged;
        }

        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            if (error != null)
            {
                Toast.MakeText(Application.Context, $"Error: {error.Message}", ToastLength.Short).Show();
                return;
            }

            var snapshot = value.JavaCast<QuerySnapshot>();
            List<UserRecords> allUsersRecordsList = new List<UserRecords>();

            foreach (var document in snapshot.Documents)
            {
                var documentData = document.Data;
                string uid = document.Id;

                string email = document.Contains("email") ? document.GetString("email") : "No email found";
                string username = document.Contains("username") ? document.GetString("username") : "No username found";
                string userRecordsJson = document.Contains("userRecords") ? document.GetString("userRecords") : null;

                Dictionary<int, int?> userRecords = new Dictionary<int, int?>();
                if (userRecordsJson != null)
                    userRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(userRecordsJson);

                int doneLevelsAmount = User.CountUserDoneLevels(userRecords);
                int totalStepsSum = User.TotalPointsSum(userRecords);

                int points = (doneLevelsAmount * 1000) - totalStepsSum;
                if (points <= 0) points = 0;

                UserRecords records = new UserRecords(username, email, userRecords, points);
                allUsersRecordsList.Add(records);
            }

            // Sort by points
            var sorted = allUsersRecordsList.OrderByDescending(u => u.Points).ToList();

            _onLeaderboardChanged?.Invoke(sorted);
        }
    }
}