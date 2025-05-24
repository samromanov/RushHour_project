using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.ViewPager2.Widget;
using RushHour_project.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RushHour_project.Lists;
using RushHour_project.Classes;
using AndroidX.RecyclerView.Widget;

namespace RushHour_project
{
    [Activity(Label = "LevelSelectionActivity")]
    public class LevelSelectionActivity : Activity
    {
        public ImageButton _levels_backBtn;
        public Button _selectLevelBtn;
        public ViewPager2 _viewPager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_levels);
            Init();
        }
        public void Init()
        {
            _viewPager = FindViewById<ViewPager2>(Resource.Id.levelViewPager);
            _levels_backBtn = FindViewById<ImageButton>(Resource.Id.levels_backBtn);
            _selectLevelBtn = FindViewById<Button>(Resource.Id.selectLevelBtn);
            //List<Level> levelsList = LevelsList.Clone();

            _levels_backBtn.Click += _levels_backBtn_Click;
            _selectLevelBtn.Click += _selectLevelBtn_Click;

            var adapter = new LevelPagerAdapter(this, LevelsList.Clone());
            _viewPager.Adapter = adapter;

            // Start somewhere in the middle to simulate infinite scroll

            _viewPager.SetCurrentItem(int.MaxValue / 2, false);

            _viewPager.SetPageTransformer(new ScalePageTransformer());
            //_viewPager.SetPageTransformer(new ZoomOutPageTransformer());



            //var recyclerView = _viewPager.GetChildAt(0) as RecyclerView;
            //recyclerView.SetPadding(100, 0, 120, 0); // this for spacing on sides
            //recyclerView.SetClipToPadding(false);
            //recyclerView.SetClipChildren(false);

            _viewPager.OffscreenPageLimit = 3;

            adapter.OnLevelClicked += level =>
            {

                Intent game_intent = new Intent(this, typeof(GameActivity));
                string stringLevelNum = level.levelNumber.ToString();
                game_intent.PutExtra("chosenLevel", stringLevelNum);
                StartActivity(game_intent);
            };

        }

        private void _selectLevelBtn_Click(object sender, EventArgs e)
        {

        }

        private void _levels_backBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}