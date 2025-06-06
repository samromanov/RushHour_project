using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RushHour_project.Classes;
using RushHour_project.Lists;
using AndroidX.ViewPager2.Widget;

namespace RushHour_project.Adapters
{
    public class LevelPagerAdapter : RecyclerView.Adapter
    {
        private Context _context;
        private List<Level> _levels;

        public event Action<Level> OnLevelClicked;

        public LevelPagerAdapter(Context context, List<Level> levels)
        {
            this._context = context;
            this._levels = levels;
        }

        public override int ItemCount => int.MaxValue; // simulate infinite

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var realPosition = position % _levels.Count;
            var viewHolder = holder as LevelViewHolder;
            var level = _levels[realPosition];

            //ViewGroup.MarginLayoutParams layoutParams = (ViewGroup.MarginLayoutParams)viewHolder.ItemView.LayoutParameters;
            //layoutParams.MarginStart = 20;
            //layoutParams.MarginEnd = 20;
            //viewHolder.ItemView.LayoutParameters = layoutParams;

            viewHolder.Bind(level);

            viewHolder.ItemView.Click += (s, e) =>
            {
                OnLevelClicked?.Invoke(level);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //var view = LayoutInflater.From(_context).Inflate(Resource.Layout.levelCard_item, parent, false);
            //return new LevelViewHolder(view);
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(Resource.Layout.pager_card_container, parent, false);
            return new LevelViewHolder(view);
        }

        

        public class LevelViewHolder : RecyclerView.ViewHolder
        {
            private readonly GridView _card_boardGrid;
            private readonly LinearLayout _card_levelBackground, _card_difficultyBackground;
            private readonly TextView _card_levelTxt, _card_difficultyTxt;

            public LevelViewHolder(View itemView) : base(itemView)
            {
                //finding views by Id
                _card_boardGrid = itemView.FindViewById<GridView>(Resource.Id.card_boardGrid);
                _card_levelBackground = itemView.FindViewById<LinearLayout>(Resource.Id.card_levelBackground);
                _card_difficultyBackground = itemView.FindViewById<LinearLayout>(Resource.Id.card_difficultyBackground);
                _card_levelTxt = itemView.FindViewById<TextView>(Resource.Id.card_levelTxt);
                _card_difficultyTxt = itemView.FindViewById<TextView>(Resource.Id.card_difficultyTxt);
            }

            public void Bind(Level level)
            {
                _card_levelTxt.Text = level.levelNumber.ToString();
                string hexColourDif = "CCCCCC", hexColourNum = "ffffff";
                switch (level.difficulty)
                {
                    case 1:
                        hexColourDif = "#a0c60e"; // light green
                        hexColourNum = "#cbda95"; // light light green
                        _card_difficultyTxt.Text = "Beginner";
                        break;
                    case 2:
                        hexColourDif = "#f4c33a"; // light orange
                        hexColourNum = "#f4e0a3"; // light light orange
                        _card_difficultyTxt.Text = "Intermediate";
                        break;
                    case 3:
                        hexColourDif = "#0087e8"; // light blue
                        hexColourNum = "#87d1ec"; // light light blue
                        _card_difficultyTxt.Text = "Advanced";
                        break;
                    case 4:
                        hexColourDif = "#ef982f"; // orange
                        hexColourNum = "#f6d58e"; // light orange
                        _card_difficultyTxt.Text = "Expert";
                        break;
                    case 5:
                        hexColourDif = "#e14326"; // red
                        hexColourNum = "#dea381"; // light red
                        _card_difficultyTxt.Text = "Grand master";
                        break;
                }
                Android.Graphics.Color difColor = Android.Graphics.Color.ParseColor(hexColourDif);
                Android.Graphics.Color numColor = Android.Graphics.Color.ParseColor(hexColourNum);
                _card_difficultyBackground.SetBackgroundColor(difColor);
                _card_levelBackground.SetBackgroundColor(numColor);


                //set up board
                level.PlaceCarsOnBoard(); // place all cars on the board of the chosen level
                _card_boardGrid.Adapter = new BoardAdapter(Application.Context, level.board, level.cars, "levels");
            }
        }
    }

}