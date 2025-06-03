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
    internal class LevelAdapter : BaseAdapter<Level>
    {

        private Context _context;
        private List<Level> _levels;


        public LevelAdapter(Context context, List<Level> levels)
        {
            this._context = context;
            this._levels = levels;
        }
        public override Level this[int position]
        {
            get { return _levels[position]; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override int Count
        {
            get { return _levels.Count; }
        }



        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var level = _levels[position]; // level in the original list in the position
            var view = convertView;
            if (view == null)
                view = LayoutInflater.From(_context).Inflate(Resource.Layout.individual_level, parent, false);

            var _levelButton = view.FindViewById<Button>(Resource.Id.levelButton);
            _levelButton.Click += (o, e) =>
            {
                Intent game_intent = new Intent(_context, typeof(GameActivity));
                string stringLevelNum = (level.levelNumber).ToString();
                game_intent.PutExtra("chosenLevel", stringLevelNum);
                _context.StartActivity(game_intent);
            };

            _levelButton.Text = level.levelNumber.ToString();


            // Define custom hex colors
            string hexColor = "#CCCCCC"; // Default gray
            switch (level.difficulty)
            {
                case 1:
                    hexColor = "#a0c60e"; // light green
                    break;
                case 2:
                    hexColor = "#f4c33a"; // light orange
                    break;
                case 3:
                    hexColor = "#0087e8"; // light blue
                    break;
                case 4:
                    hexColor = "#ef982f"; // orange
                    break;
                case 5:
                    hexColor = "#e14326"; // red
                    break;
            }
            Android.Graphics.Color color = Android.Graphics.Color.ParseColor(hexColor);
            _levelButton.SetBackgroundColor(color);

            return view;
        }
    }
}