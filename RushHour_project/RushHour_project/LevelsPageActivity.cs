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
using RushHour_project.Adapters;
using RushHour_project.Lists;

namespace RushHour_project
{
    [Activity(Label = "LevelsPageActivity")]
    public class LevelsPageActivity : Activity
    {
        private ListView _levelsListView;
        private LevelAdapter _levelAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.levels_page);
            Init();
        }
        void Init()
        {
            _levelsListView = FindViewById<ListView>(Resource.Id.levelsListView);

            _levelAdapter = new LevelAdapter(this, LevelsList.levelsList);
            _levelsListView.Adapter = _levelAdapter;
        }
    }
}