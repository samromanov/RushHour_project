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
using Android.Graphics;

namespace RushHour_project.Adapters
{
    public class BoardAdapter : BaseAdapter<char>
    {

        private readonly Context _context;
        private readonly char[,] _board;
        private readonly Dictionary<char, string> carColors;
        private readonly List<Car> _cars;

        public BoardAdapter(Context context, char[,] board, List<Car> cars)
        {
            this._context = context;
            this._board = board;
            this._cars = cars;

            // Define car colors (hex code)
            carColors = new Dictionary<char, string>
            {
                { '.', "#3c4246" }, // Light grey for empty
                { 'X', "#f02228" }, // red car (target)
                { 'A', "#92cb88" }, // light green car
                { 'B', "#f7862c" }, // orange car
                { 'C', "#22b9ec" }, // light blue car
                { 'D', "#ee7e9f" }, // pink car
                { 'E', "#6764ae" }, // purple car
                { 'F', "#12986d" }, // dark green car
                { 'G', "#d9dbdd" }, // light gray car
                { 'H', "#edd78b" }, // beige car
                { 'I', "#fff554" }, // light yellow car
                { 'J', "#946959" }, // brown car
                { 'K', "#8c8d0b" }, // green car
                { 'O', "#fed800" }, // yellow lorry
                { 'P', "#9e7fbe" }, // purple lorry
                { 'Q', "#0a7eb9" }, // blue lorry
                { 'R', "#02ac95" } // turquoise lorry
            };
        }

        //Makes it easy to work with the board as a flat list (which is how GridView works), even though the board is a 2D array.
        public override char this[int position]
        {
            get
            {
                int row = position / 6;
                int col = position % 6;
                return _board[row, col];
            }
        }


        //Returns the total number of cells in the board(should be 36 for a 6x6 grid).
        //Tells the adapter how many items(cells) to display.
        public override int Count => _board.Length;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.grid_item, parent, false);
            FrameLayout cell_item = view.FindViewById<FrameLayout>(Resource.Id.cell_item);

            int row = position / 6;
            int col = position % 6;
            char cell = _board[row, col];

            if (cell == '.')
            {
                cell_item.SetBackgroundResource(Resource.Drawable.emptyCell);
            }
            else
            {
                // Find the car object
                Car car = _cars.FirstOrDefault(c => c.Id == cell);
                if (car != null)
                {
                    int carPart = 0; // 0 = start, 1 = middle (for length 3), 2 = end 
                    if (car.IsHorizontal)
                    {
                        carPart = col - car.Column;
                    }
                    else
                    {
                        carPart = row - car.Row;
                    }

                    // Now we know which part of the car this cell is!
                    switch (carPart)
                    {
                        case 0: // start
                            if (car.IsHorizontal)
                            {

                            }
                            else
                            {

                            }

                            break;
                        case 1: // middle

                            break;
                        case 2: // end

                            break;
                        
                            
                    }

                    

                    
                }
                else
                {
                    // fallback if car not found
                    cell_item.SetBackgroundColor(Color.ParseColor(carColors.ContainsKey(cell) ? carColors[cell] : "#AAAAAA"));
                }
            }

            return view;
        }

    }
}