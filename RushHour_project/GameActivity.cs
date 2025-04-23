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
        int chosenLevel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_game);
            Init();
        }
        void Init()
        {
            chosenLevel = int.Parse(Intent.GetStringExtra("chosenLevel")); // fetch the chosen level number from the intent
            //string cars_level_var = "cars_level" + Intent.GetStringExtra("chosenLevel");
            Level currentLevel = new Level();
            foreach (var level in LevelsList.levelsList)
            {
                if (level.levelNumber == chosenLevel)
                {
                    currentLevel = level;
                    break;
                }
            }

            //var currentLevel = LevelsList.levelsList[chosenLevel - 1]; // fetch the level from the LevelsList by the chosenLevel
            currentLevel.PlaceCarsOnBoard();

            var grid = FindViewById<GridView>(Resource.Id.boardGrid);
            grid.Adapter = new BoardAdapter(this, currentLevel.board);


        }
    }
}