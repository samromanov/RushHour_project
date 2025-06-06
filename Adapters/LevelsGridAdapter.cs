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
using Newtonsoft.Json;

namespace RushHour_project.Adapters
{
    public class LevelsGridAdapter : BaseAdapter<Level>
    {
        private readonly Context _context;
        private readonly List<Level> _levels;
        private readonly bool _isLoggedIn;

        public LevelsGridAdapter(Context context, List<Level> levels, bool isLoggedIn)
        {
            this._context = context;
            this._levels = levels;
            this._isLoggedIn = isLoggedIn;
        }

        public override Level this[int position] => _levels[position];
        public override int Count => _levels.Count;
        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(_context).Inflate(Resource.Layout.levels_grid_item, parent, false);
            }

            var level = _levels[position];

            // Set your item views here
            var _grid_levelBackground = convertView.FindViewById<LinearLayout>(Resource.Id.grid_levelBackground);
            var _grid_levelNumber = convertView.FindViewById<TextView>(Resource.Id.grid_levelNumber);
            var _grid_levelStatusBackground = convertView.FindViewById<LinearLayout>(Resource.Id.grid_levelStatusBackground);
            //var _grid_levelStatus = convertView.FindViewById<TextView>(Resource.Id.grid_levelStatus);
            var _star1 = convertView.FindViewById<ImageView>(Resource.Id.level_star1);
            var _star2 = convertView.FindViewById<ImageView>(Resource.Id.level_star2);
            var _star3 = convertView.FindViewById<ImageView>(Resource.Id.level_star3);

            _grid_levelNumber.Text = level.levelNumber.ToString(); // set the text as current level number
            string hexColourDif = "CCCCCC", hexColourNum = "ffffff";
            switch (level.difficulty)
            {
                case 1: // beginner
                    hexColourDif = "#a0c60e"; // light green
                    hexColourNum = "#cbda95"; // light light green
                    break;
                case 2: // intermediate
                    hexColourDif = "#f4c33a"; // light orange
                    hexColourNum = "#f4e0a3"; // light light orange
                    break;
                case 3: // advanced
                    hexColourDif = "#0087e8"; // light blue
                    hexColourNum = "#87d1ec"; // light light blue
                    break;
                case 4: // expert
                    hexColourDif = "#ef982f"; // orange
                    hexColourNum = "#f6d58e"; // light orange
                    break;
                case 5: // grand master
                    hexColourDif = "#e14326"; // red
                    hexColourNum = "#dea381"; // light red
                    break;
            }
            Android.Graphics.Color difColor = Android.Graphics.Color.ParseColor(hexColourDif);
            Android.Graphics.Color numColor = Android.Graphics.Color.ParseColor(hexColourNum);
            _grid_levelStatusBackground.SetBackgroundColor(difColor);
            _grid_levelBackground.SetBackgroundColor(numColor);


            ISharedPreferences prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);
            if (_isLoggedIn == false)
            {
                Dictionary<int, int?> localUserRecords = null;
                string localUserRecords_json = prefs.GetString("localUserRecords", null);
                if (localUserRecords_json == null) // local records dictionary is initially empty 
                {
                    localUserRecords = new Dictionary<int, int?>();

                    // Initialise with nulls for 60 levels
                    for (int i = 1; i <= 60; i++)
                        localUserRecords[i] = null;
                    string userRecords_json = JsonConvert.SerializeObject(localUserRecords);

                    var prefsEditor = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private).Edit();
                    prefsEditor.PutString("localUserRecords", userRecords_json);

                    prefsEditor.Apply(); //apply changes on the currentUserFile sharedpreference

                }
                else // local records dictionary is not empty 
                {
                    localUserRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(localUserRecords_json);

                    if (localUserRecords == null)
                    {
                        localUserRecords = new Dictionary<int, int?>();

                        for (int i = 1; i <= 60; i++)
                            localUserRecords[i] = null;
                    }

                    if (localUserRecords[level.levelNumber] != null)
                    {
                        int score = 1000 - (int)localUserRecords[level.levelNumber];
                        // STARS HENDLING
                        if (score >= 0 && score < 750) // one star
                        {
                            _star1.Visibility = ViewStates.Visible;
                            _star2.Visibility = ViewStates.Gone;
                            _star3.Visibility = ViewStates.Gone;
                        }
                        else if (score >= 750 && score < 950) // two stars
                        {
                            _star1.Visibility = ViewStates.Visible;
                            _star2.Visibility = ViewStates.Visible;
                            _star3.Visibility = ViewStates.Gone;
                        }
                        else if (score >= 950 && score < 1000) // three stars
                        {
                            _star1.Visibility = ViewStates.Visible;
                            _star2.Visibility = ViewStates.Visible;
                            _star3.Visibility = ViewStates.Visible;
                        }
                    }
                    else
                    {
                        _star1.Visibility = ViewStates.Gone;
                        _star2.Visibility = ViewStates.Gone;
                        _star3.Visibility = ViewStates.Gone;
                    }
                }
            }
            else
            {
                string userRecords_json = prefs.GetString("userRecords", null);

                Dictionary<int,int?> connectedUserRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(userRecords_json);

                if (connectedUserRecords[level.levelNumber] != null) // won this game once
                {
                    int score = 1000 - (int)connectedUserRecords[level.levelNumber];
                    // STARS HENDLING
                    if (score >= 0 && score < 750) // one star
                    {
                        _star1.Visibility = ViewStates.Visible;
                        _star2.Visibility = ViewStates.Gone;
                        _star3.Visibility = ViewStates.Gone;
                    }
                    else if (score >= 750 && score < 950) // two stars
                    {
                        _star1.Visibility = ViewStates.Visible;
                        _star2.Visibility = ViewStates.Visible;
                        _star3.Visibility = ViewStates.Gone;
                    }
                    else if (score >= 950 && score < 1000) // three stars
                    {
                        _star1.Visibility = ViewStates.Visible;
                        _star2.Visibility = ViewStates.Visible;
                        _star3.Visibility = ViewStates.Visible;
                    }
                }
                else
                {
                    _star1.Visibility = ViewStates.Gone;
                    _star2.Visibility = ViewStates.Gone;
                    _star3.Visibility = ViewStates.Gone;
                }

            }
            return convertView;
        }
    }
}