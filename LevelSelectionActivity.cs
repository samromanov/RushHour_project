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
using static RushHour_project.Adapters.LevelPagerAdapter;
using RushHour_project.Sounds;

namespace RushHour_project
{
    [Activity(Label = "LevelSelectionActivity")]
    public class LevelSelectionActivity : Activity
    {
        public ImageButton _levels_backBtn, _changeViewBtn;
        public Button _selectLevelBtn;
        public ViewPager2 _viewPager;
        public LinearLayout _levelsGridLayout, _levelsCarouselLayout;
        public GridView _levelsGridView;
        public bool isGridView = true, isLoggedIn = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_levels);
            Init();
        }
        protected override void OnResume()
        {
            base.OnResume();
            var intent = new Intent(this, typeof(MusicService));
            intent.PutExtra("MusicResId", Resource.Raw.general);
            StartService(intent);
        }
        public void Init()
        {
            _viewPager = FindViewById<ViewPager2>(Resource.Id.levelViewPager);
            _levels_backBtn = FindViewById<ImageButton>(Resource.Id.levels_backBtn);
            _selectLevelBtn = FindViewById<Button>(Resource.Id.selectLevelBtn);
            _levelsGridLayout = FindViewById<LinearLayout>(Resource.Id.levelsGridLayout);
            _changeViewBtn = FindViewById<ImageButton>(Resource.Id.changeViewBtn);
            _levelsCarouselLayout = FindViewById<LinearLayout>(Resource.Id.levelsCarouselLayout);
            _levelsGridView = FindViewById<GridView>(Resource.Id.levelsGridView);


            //List<Level> levelsList = LevelsList.Clone();
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);
            isLoggedIn = prefs.GetBoolean("isLoggedIn", false);
            int savedIndex = 0;
            if (isLoggedIn == true)
            {
                savedIndex=prefs.GetInt("connected_last_level_index", 0);
            }
            else
            {
                savedIndex= prefs.GetInt("offline_last_level_index", 0);
            }

            _levelsGridView.Adapter = new LevelsGridAdapter(this, LevelsList.Clone(), isLoggedIn);

            _levels_backBtn.Click += _levels_backBtn_Click;
            _selectLevelBtn.Click += _selectLevelBtn_Click;
            _changeViewBtn.Click += _changeViewBtn_Click;

            if (isGridView == true) //grid view
            {
                _levelsGridLayout.Visibility = ViewStates.Visible;
                _levelsCarouselLayout.Visibility = ViewStates.Gone;
                _changeViewBtn.SetBackgroundResource(Resource.Drawable.carousel_view);
            }
            else // carousel view
            {
                _levelsGridLayout.Visibility = ViewStates.Gone;
                _levelsCarouselLayout.Visibility = ViewStates.Visible;
                _changeViewBtn.SetBackgroundResource(Resource.Drawable.grid_view);
            }

            var adapter = new LevelPagerAdapter(this, LevelsList.Clone());
            _viewPager.Adapter = adapter;

            // Start somewhere in the middle to simulate infinite scroll
            //_viewPager.SetCurrentItem(int.MaxValue / 2, false);

            _viewPager.SetPageTransformer(new ScalePageTransformer());

            // This ensures the item appears centered
            int initialPage = int.MaxValue / 2 - ((int.MaxValue / 2) % LevelsList.levelsList.Count) + savedIndex;
            _viewPager.SetCurrentItem(initialPage, false);

            // Listen for page changes and save real index
            _viewPager.RegisterOnPageChangeCallback(new ViewPager2OnPageChangeCallback(isLoggedIn, LevelsList.levelsList.Count, this));

            _viewPager.OffscreenPageLimit = 3;

            adapter.OnLevelClicked += level =>
            {
                SoundEffectManager.Play("click");
                MusicService.StopMusic();
                Intent game_intent = new Intent(this, typeof(GameActivity));
                string stringLevelNum = level.levelNumber.ToString();
                game_intent.PutExtra("chosenLevel", stringLevelNum);
                StartActivity(game_intent);
            };
            // Handle item clicks
            _levelsGridView.ItemClick += (sender, e) =>
            {
                SoundEffectManager.Play("click");
                MusicService.StopMusic();
                var clickedLevel = LevelsList.Clone()[e.Position];
                Intent game_intent = new Intent(this, typeof(GameActivity));
                string stringLevelNum = clickedLevel.levelNumber.ToString();
                game_intent.PutExtra("chosenLevel", stringLevelNum);
                StartActivity(game_intent);
            };
        }

        private void _changeViewBtn_Click(object sender, EventArgs e)
        {
            SoundEffectManager.Play("click");
            if (isGridView == true) // it was a grid view - needs to change to carousel view
            {
                _levelsGridLayout.Visibility = ViewStates.Gone;
                _levelsCarouselLayout.Visibility = ViewStates.Visible;
                _changeViewBtn.SetBackgroundResource(Resource.Drawable.grid_view);

                isGridView = false;
            }
            else // it was a carousel view - needs to change to grid view
            {
                _levelsGridLayout.Visibility = ViewStates.Visible;
                _levelsCarouselLayout.Visibility = ViewStates.Gone;
                _changeViewBtn.SetBackgroundResource(Resource.Drawable.carousel_view);

                isGridView = true;
            }
        }

        private void _selectLevelBtn_Click(object sender, EventArgs e)
        {

        }

        private void _levels_backBtn_Click(object sender, EventArgs e)
        {
            SoundEffectManager.Play("click");
            Finish();
        }
    }
    public class ViewPager2OnPageChangeCallback : ViewPager2.OnPageChangeCallback
    {
        private int _levelCount;
        private Context _context;
        private bool _isLoggedIn;

        public ViewPager2OnPageChangeCallback(bool isLoggedIn, int levelCount, Context context)
        {
            _levelCount = levelCount;
            _context = context;
            _isLoggedIn = isLoggedIn;
        }

        public override void OnPageSelected(int position)
        {
            base.OnPageSelected(position);

            int realIndex = position % _levelCount;
            ISharedPreferences prefs = _context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();
            if (_isLoggedIn == false)
            {
                editor.PutInt("offline_last_level_index", realIndex);
            }
            else
            {
                editor.PutInt("connected_last_level_index", realIndex);
            }
            
            editor.Apply();
        }
    }
}