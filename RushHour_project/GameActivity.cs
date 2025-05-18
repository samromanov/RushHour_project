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
        //buttons
        public Button _lastLevelBtn, _nextLevelBtn; // go to the next or previous level
        public Button _resetBtn, _undoBtn; // top bar actions
        public Button _game_backBtn, _game_solutionBtn, _game_homeBtn; // bottom bar actions

        //text views
        public TextView _gameDifficultyTxt, _gameLevelTxt; //current level
        public TextView _stepsRecordTxt; //minimum steps record
        public TextView _stepCounter_hundredsDigit, _stepCounter_tensDigit, _stepCounter_onesDigit; //steps counter


        //fetch information from other pages
        public int CHOSEN_LEVEL;
        public GridView _boardGrid;

        //properties
        public Level cloned_chosenLevel = new Level(); // cloned leve (cars can be moved)
        public Level originalLevel = new Level(); //the original level (cars not touched)

        //Track touch state
        private bool isDragging = false;
        private Car selectedCar = null;
        private int startRow, startCol;
        private float startX, startY;

        //Counter
        private int moveCount = 0;
        private Car lastMovedCar = null;
        private int lastCarStartRow, lastCarStartCol;
        //private int lastCarEndRow, lastCarEndCol;
        private bool carMoved = false;

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
                    originalLevel = level.Clone();
                    break;
                }
            }

            //var currentLevel = LevelsList.levelsList[chosenLevel - 1]; // fetch the level from the LevelsList by the chosenLevel
            cloned_chosenLevel.PlaceCarsOnBoard();

            //Finding views by ID:
            _lastLevelBtn = FindViewById<Button>(Resource.Id.lastLevelBtn);
            _nextLevelBtn = FindViewById<Button>(Resource.Id.nextLevelBtn);
            _resetBtn = FindViewById<Button>(Resource.Id.resetBtn);
            _undoBtn = FindViewById<Button>(Resource.Id.undoBtn);
            _game_backBtn = FindViewById<Button>(Resource.Id.game_backBtn);
            _game_solutionBtn = FindViewById<Button>(Resource.Id.game_solutionBtn);
            _game_homeBtn = FindViewById<Button>(Resource.Id.game_homeBtn);
            _gameDifficultyTxt = FindViewById<TextView>(Resource.Id.gameDifficultyTxt);
            _gameLevelTxt = FindViewById<TextView>(Resource.Id.gameLevelTxt);
            _stepsRecordTxt = FindViewById<TextView>(Resource.Id.stepsRecordTxt);
            _stepCounter_onesDigit = FindViewById<TextView>(Resource.Id.stepCounter_onesDigit);
            _stepCounter_tensDigit = FindViewById<TextView>(Resource.Id.stepCounter_tensDigit);
            _stepCounter_hundredsDigit = FindViewById<TextView>(Resource.Id.stepCounter_hundredsDigit);
            _boardGrid = FindViewById<GridView>(Resource.Id.boardGrid);


            //Board set up
            _boardGrid.Adapter = new BoardAdapter(this, cloned_chosenLevel.board, cloned_chosenLevel.cars);
            _boardGrid.Touch += BoardGrid_Touch;

            //button clicks implementations
            _game_homeBtn.Click += _game_homeBtn_Click;
            _game_backBtn.Click += _game_backBtn_Click;
            _resetBtn.Click += _resetBtn_Click;

        }
        /// <summary>
        /// Updates the hundreds, tens, and ones TextViews to display the current move count.
        /// </summary>
        /// <param name="moves">The current number of moves to display.</param>
        private void SetMoveCounter(int moves)
        {
            int ones = moves % 10;
            int tens = (moves / 10) % 10;
            int hundreds = (moves / 100) % 10;

            _stepCounter_onesDigit.Text = ones.ToString();
            _stepCounter_tensDigit.Text = tens.ToString();
            _stepCounter_hundredsDigit.Text = hundreds.ToString();
        }
        private void _resetBtn_Click(object sender, EventArgs e)
        {
            // Reset the current level to the original state
            cloned_chosenLevel = originalLevel.Clone(); // Deep copy again

            // Place cars on the board
            cloned_chosenLevel.PlaceCarsOnBoard();

            // Reset the move counter variable and update the display
            moveCount = 0;
            SetMoveCounter(moveCount);

            // Update the board UI
            _boardGrid.Adapter = new BoardAdapter(this, cloned_chosenLevel.board, cloned_chosenLevel.cars);
            (_boardGrid.Adapter as BoardAdapter).NotifyDataSetChanged();
        }

        private void _game_backBtn_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        /// <summary>
        /// Handles the system back button press. Navigates back to the levels list activity,
        /// clearing any activities above it from the stack, and closes the current game activity.
        /// </summary>
        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(LevelsPageActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            StartActivity(intent);
            Finish();
        }
        private void _game_homeBtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            StartActivity(intent);
            Finish(); // finish the current activity
        }

        private void BoardGrid_Touch(object sender, View.TouchEventArgs e)
        {
            var ev = e.Event;
            int action = (int)(ev.Action & MotionEventActions.Mask);

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

                    // Track the car's original position for step counting
                    lastMovedCar = selectedCar;
                    lastCarStartRow = selectedCar.Row;
                    lastCarStartCol = selectedCar.Column;
                    carMoved = false;
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
                            carMoved = true;
                        }
                        // Reset startX/startY to current position to allow further dragging
                        startX = ev.GetX();
                        startY = ev.GetY();
                    }
                    break;

                case (int)MotionEventActions.Up:
                case (int)MotionEventActions.Cancel:
                    if (lastMovedCar != null && carMoved)
                    {
                        int endRow = lastMovedCar.Row;
                        int endCol = lastMovedCar.Column;
                        int delta = Math.Abs(endRow - lastCarStartRow) + Math.Abs(endCol - lastCarStartCol);

                        if (delta != 0)
                        {
                            // If the car is returned to its original position, decrement
                            if (endRow == lastCarStartRow && endCol == lastCarStartCol)
                                moveCount--;
                            else
                                moveCount++;

                            SetMoveCounter(moveCount);
                        }
                    }
                    isDragging = false;
                    selectedCar = null;
                    lastMovedCar = null;
                    carMoved = false;
                    break;
            }
        (_boardGrid.Adapter as BoardAdapter).NotifyDataSetChanged();
        }
    }
}