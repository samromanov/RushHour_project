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
using RushHour_project.Adapters;
using RushHour_project.Lists;

namespace RushHour_project
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        //fetch information from other pages
        public int CHOSEN_LEVEL;
        public GridView _boardGrid;

        //properties
        public Level cloned_chosenLevel = new Level();

        //Track touch state
        private bool isDragging = false;
        private Car selectedCar = null;
        private int startRow, startCol;
        private float startX, startY;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_game);
            Init();
        }
        public void Init()
        {
            CHOSEN_LEVEL = int.Parse(Intent.GetStringExtra("chosenLevel")); // fetch the chosen level number from the intent


            //finding the level
            foreach (var level in LevelsList.levelsList)
            {
                if (level.levelNumber == CHOSEN_LEVEL)
                {
                    cloned_chosenLevel = level.Clone();
                    break;
                }
            }

            //var currentLevel = LevelsList.levelsList[chosenLevel - 1]; // fetch the level from the LevelsList by the chosenLevel
            cloned_chosenLevel.PlaceCarsOnBoard();

            _boardGrid = FindViewById<GridView>(Resource.Id.boardGrid);
            _boardGrid.Adapter = new BoardAdapter(this, cloned_chosenLevel.board, cloned_chosenLevel.cars);
            _boardGrid.Touch += BoardGrid_Touch;


        }
        private void BoardGrid_Touch(object sender, View.TouchEventArgs e)
        {
            var ev = e.Event;
            int action = (int)(ev.Action & MotionEventActions.Mask); //

            switch (action)
            {
                case (int)MotionEventActions.Down:
                    // Get the touched cell
                    int position = _boardGrid.PointToPosition((int)ev.GetX(), (int)ev.GetY());
                    if (position == AdapterView.InvalidPosition) return;

                    startRow = position / 6;
                    startCol = position % 6;
                    char carId = cloned_chosenLevel.board[startRow, startCol];
                    if (carId == '.') return; // empty cell

                    selectedCar = cloned_chosenLevel.cars.FirstOrDefault(c => c.Id == carId);
                    if (selectedCar == null) return;

                    isDragging = true;
                    startX = ev.GetX();
                    startY = ev.GetY();
                    break;

                case (int)MotionEventActions.Move:
                    if (!isDragging || selectedCar == null) return;

                    float deltaX = ev.GetX() - startX;
                    float deltaY = ev.GetY() - startY;

                    int cellDelta = 0;
                    if (selectedCar.IsHorizontal)
                    {
                        cellDelta = (int)((ev.GetX() - startX) / _boardGrid.GetChildAt(0).Width);
                    }
                    else
                    {
                        cellDelta = (int)((ev.GetY() - startY) / _boardGrid.GetChildAt(0).Height);
                    }

                    if (cellDelta != 0)
                    {
                        if (cloned_chosenLevel.TryMoveCar(selectedCar.Id, cellDelta))
                        {
                            (_boardGrid.Adapter as BoardAdapter).NotifyDataSetChanged();
                        }
                        // Reset startX/startY to current position to allow further dragging
                        startX = ev.GetX();
                        startY = ev.GetY(); 
                    }
                    break;

                case (int)MotionEventActions.Up:
                case (int)MotionEventActions.Cancel:
                    isDragging = false;
                    selectedCar = null;
                    break;
            }
            (_boardGrid.Adapter as BoardAdapter).NotifyDataSetChanged();
        }
    }
}