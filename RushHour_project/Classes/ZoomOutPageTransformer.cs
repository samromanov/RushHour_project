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
    public class ZoomOutPageTransformer : Java.Lang.Object, ViewPager2.IPageTransformer
    {
        public void TransformPage(View page, float position)
        {
            float scale = 0.85f;
            float alpha = 0.5f;

            if (position < -1)
            {
                page.Alpha = 0f;
            }
            else if (position <= 1)
            {
                float scaleFactor = Math.Max(scale, 1 - Math.Abs(position));
                float vertMargin = page.Height * (1 - scaleFactor) / 2;
                float horzMargin = page.Width * (1 - scaleFactor) / 2;

                page.TranslationX = position < 0
                    ? horzMargin - vertMargin / 2
                    : -horzMargin + vertMargin / 2;

                page.ScaleX = scaleFactor;
                page.ScaleY = scaleFactor;
                page.Alpha = alpha + (scaleFactor - scale) / (1 - scale) * (1 - alpha);
            }
            else
            {
                page.Alpha = 0f;
            }
        }
    }
}