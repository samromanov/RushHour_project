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
    public class Advanced : Level
    {
        public Advanced(int difficulty, int levelNumber, List<Car> cars) : base(difficulty, levelNumber, cars)
        {

        }
    }
}