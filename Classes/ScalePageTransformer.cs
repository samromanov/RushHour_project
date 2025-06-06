using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.ViewPager2.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RushHour_project.Classes
{
    public class ScalePageTransformer : Java.Lang.Object, ViewPager2.IPageTransformer
    {
        public void TransformPage(View page, float position)
        {
            float scale = 0.85f + (1 - Math.Abs(position)) * 0.15f;
            page.ScaleX = scale;
            page.ScaleY = scale;
            page.Alpha = 0.5f + (1 - Math.Abs(position)) * 0.5f;
        }
    }

}