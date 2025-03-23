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

namespace RushHour_project.Classes
{
    public class Level
    {
        private int difficulty; // (eg. 1 = beginner, 2 = intermediate, 5 = grand master...) 
        private int levelNumber; // number of the level
        private char[,] boardGrid; // A 6x6 matrix representing the parking lot ("0" = empty, other chars for different vehicles)

    }
}