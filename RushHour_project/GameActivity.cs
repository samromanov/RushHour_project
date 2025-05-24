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
using Newtonsoft.Json;
using Firebase.Auth;

namespace RushHour_project
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        //buttons
        public Button _lastLevelBtn, _nextLevelBtn; // go to the next or previous level
        public Button _resetBtn; // top bar actions
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

        //authentication
        public bool isLoggedIn = false;
        public string _username, _email;

        //User records dictionaries
        public Dictionary<int, int?> connectedUserRecords = null; //if signed in
        public Dictionary<int, int?> localUserRecords = null; //if not signed in
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

            cloned_chosenLevel.PlaceCarsOnBoard(); // place all cars on the board of the chosen level

            //Finding views by ID:
            _lastLevelBtn = FindViewById<Button>(Resource.Id.lastLevelBtn);
            _nextLevelBtn = FindViewById<Button>(Resource.Id.nextLevelBtn);
            _resetBtn = FindViewById<Button>(Resource.Id.resetBtn);
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

            //Update chosen level label
            string hexColor = "#ffffff"; // Default white
            switch (originalLevel.difficulty)
            {
                case 1: //Beginner
                    _gameDifficultyTxt.Text = "Beginner";
                    hexColor = "#a0c60e"; // light green
                    break;
                case 2: //Intermediate
                    _gameDifficultyTxt.Text = "Intermediate";
                    hexColor = "#f4c33a"; // light orange
                    break;
                case 3: //Advanced
                    _gameDifficultyTxt.Text = "Advanced";
                    hexColor = "#0087e8"; // light blue
                    break;
                case 4: //Expert
                    _gameDifficultyTxt.Text = "Expert";
                    hexColor = "#ef982f"; // orange
                    break;
                case 5: //Grand master
                    _gameDifficultyTxt.Text = "Grand master";
                    hexColor = "#e14326"; // red
                    break;
            }
            Android.Graphics.Color color = Android.Graphics.Color.ParseColor(hexColor);
            _gameDifficultyTxt.SetTextColor(color); //setting the colour of the difficulty text regarding the level's difficulty
            _gameLevelTxt.Text = $"#{CHOSEN_LEVEL.ToString()}"; //setting the level's number text


            //Board set up
            _boardGrid.Adapter = new BoardAdapter(this, cloned_chosenLevel.board, cloned_chosenLevel.cars, "game");
            _boardGrid.Touch += BoardGrid_Touch;

            //button clicks implementations
            _game_homeBtn.Click += _game_homeBtn_Click;
            _game_backBtn.Click += _game_backBtn_Click;
            _resetBtn.Click += _resetBtn_Click;
            _lastLevelBtn.Click += _lastLevelBtn_Click;
            _nextLevelBtn.Click += NextLevelBtn_Click;

            if (originalLevel.levelNumber == 1)
            {
                _lastLevelBtn.Visibility = ViewStates.Invisible;
            }
            else if (originalLevel.levelNumber == 60)
            {
                _nextLevelBtn.Visibility = ViewStates.Invisible;
            }
            else
            {
                _lastLevelBtn.Visibility = ViewStates.Visible;
                _nextLevelBtn.Visibility = ViewStates.Visible;
            }

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private);
            isLoggedIn = prefs.GetBoolean("isLoggedIn", false);

            if (isLoggedIn == false)
            {
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

                    if (localUserRecords.ContainsKey(CHOSEN_LEVEL))
                    {
                        var record = localUserRecords[CHOSEN_LEVEL];
                        _stepsRecordTxt.Text = record.HasValue ? record.ToString() : "none";
                    }
                    else
                    {
                        _stepsRecordTxt.Text = "none";
                    }
                }
            }
            else
            {
                string userRecords_json = prefs.GetString("userRecords", null);

                connectedUserRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(userRecords_json);

                if (connectedUserRecords.ContainsKey(CHOSEN_LEVEL)) 
                {
                    if (connectedUserRecords[CHOSEN_LEVEL] == null) // didn't won this game yet (there's no records)
                    {
                        _stepsRecordTxt.Text = "none";
                    }
                    else
                    {
                        _stepsRecordTxt.Text = connectedUserRecords[CHOSEN_LEVEL].ToString();
                    }
                }
                else
                {
                    _stepsRecordTxt.Text = "none";
                }
            }
        }

        //opens the next level (if the current is not the last)
        private void NextLevelBtn_Click(object sender, EventArgs e)
        {
            if (CHOSEN_LEVEL == 60)
            {
                Toast.MakeText(this, "This is the last level!", ToastLength.Short).Show();
            }
            else
            {
                int goToLevel = CHOSEN_LEVEL + 1;
                if (!LevelsList.LevelExist(goToLevel))  // if the level doesn't exist in the list
                {
                    while (goToLevel < 60) 
                    {
                        goToLevel += 1;
                        if (LevelsList.LevelExist(goToLevel))
                        {
                            break;
                        }
                    }
                }
                Intent intent = new Intent(this, typeof(GameActivity));
                intent.PutExtra("chosenLevel", goToLevel.ToString());
                Finish(); // closes current activity before opening next
                StartActivity(intent);
            }
        }

        //opens the previous level (if the current is not the first)
        private void _lastLevelBtn_Click(object sender, EventArgs e)
        {
            if (CHOSEN_LEVEL == 1)
            {
                Toast.MakeText(this, "This is the first level!", ToastLength.Short).Show();
            }
            else
            {
                int goToLevel = CHOSEN_LEVEL - 1;
                if (!LevelsList.LevelExist(goToLevel))  // if the level doesn't exist in the list
                {
                    while (goToLevel > 1)
                    {
                        goToLevel -= 1;
                        if (LevelsList.LevelExist(goToLevel))
                        {
                            break;
                        }
                    }
                }
                Intent intent = new Intent(this, typeof(GameActivity));
                intent.PutExtra("chosenLevel", goToLevel.ToString());
                Finish(); // closes current activity before opening next
                StartActivity(intent);
            }
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
            ResetBoard();
        }

        public void ResetBoard()
        {
            // Reset the current level to the original state
            cloned_chosenLevel = originalLevel.Clone(); // Deep copy again

            // Place cars on the board
            cloned_chosenLevel.PlaceCarsOnBoard();

            // Reset the move counter variable and update the display
            moveCount = 0;
            SetMoveCounter(moveCount);

            // Update the board UI
            _boardGrid.Adapter = new BoardAdapter(this, cloned_chosenLevel.board, cloned_chosenLevel.cars, "game");
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
            Intent intent = new Intent(this, typeof(LevelSelectionActivity));
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

                            if (moveCount > 999)
                            {
                                Toast.MakeText(this, "You're a looser! Too many steps...",ToastLength.Long).Show();
                                ResetBoard();
                            }
                            else
                            {
                                SetMoveCounter(moveCount);
                            }
                        }
                    }
                    isDragging = false;
                    selectedCar = null;
                    lastMovedCar = null;
                    carMoved = false;

                    if (cloned_chosenLevel.IsWin() == true)
                    {
                        var winDialog = new Dialog(this);
                        winDialog.SetContentView(Resource.Layout.winScreen);

                        var editor = Application.Context.GetSharedPreferences("currentUserFile", FileCreationMode.Private).Edit();
                        if (isLoggedIn == true)
                        {

                            User user = new User(_email);
                            if (connectedUserRecords != null && connectedUserRecords.ContainsKey(CHOSEN_LEVEL)) 
                            {
                                if (connectedUserRecords[CHOSEN_LEVEL] == null || moveCount < connectedUserRecords[CHOSEN_LEVEL])
                                {
                                    connectedUserRecords[CHOSEN_LEVEL] = moveCount;
                                }
                            }                            
                            string userRecords_json = JsonConvert.SerializeObject(connectedUserRecords);

                            editor.PutString("userRecords", userRecords_json);

                            user.UpdateUserRecords(connectedUserRecords);
                        }
                        else // isLoggedIn == false
                        {
                            if (localUserRecords != null && localUserRecords.ContainsKey(CHOSEN_LEVEL))
                            {
                                if (localUserRecords[CHOSEN_LEVEL] == null || moveCount < localUserRecords[CHOSEN_LEVEL])
                                {
                                    localUserRecords[CHOSEN_LEVEL] = moveCount;
                                }
                            }
                            string userRecords_json = JsonConvert.SerializeObject(localUserRecords);
                            editor.PutString("localUserRecords", userRecords_json);

                        }
                        editor.Apply();

                        winDialog.Show();
                    }
                    break;
            }
        (_boardGrid.Adapter as BoardAdapter).NotifyDataSetChanged();
        }
    }
}