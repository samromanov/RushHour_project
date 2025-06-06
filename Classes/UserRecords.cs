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

namespace RushHour_project.Classes
{
    public class UserRecords
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public Dictionary<int, int?> Records { get; set; }
        public int Points { get; set; }

        public UserRecords(string username, string email, Dictionary<int, int?> records, int points)
        {
            this.Username = username;
            this.Email = email;
            this.Records = records;
            this.Points = points;
        }
        public int LevelsCompleted => Records.Count;
        public int TotalSteps => Records.Values.Sum(v => v ?? int.MaxValue);
    }
}