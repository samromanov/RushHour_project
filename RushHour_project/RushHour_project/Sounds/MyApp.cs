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

namespace RushHour_project.Sounds
{
    [Application]
    public class MyApp : Application, Application.IActivityLifecycleCallbacks
    {
        private int activityReferences = 0;
        private bool isActivityChangingConfigurations = false;

        public MyApp(System.IntPtr handle, JniHandleOwnership transer) : base(handle, transer) { }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
        }
        public static bool HasShownSplash = false;
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState) { }
        public void OnActivityDestroyed(Activity activity) { }
        public void OnActivityPaused(Activity activity) { }
        public void OnActivityResumed(Activity activity) { }
        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) { }
        public void OnActivityStarted(Activity activity)
        {
            if (++activityReferences == 1 && !isActivityChangingConfigurations)
            {
                // App enters foreground
                MusicService.ResumeMusic();
            }
        }
        public void OnActivityStopped(Activity activity)
        {
            isActivityChangingConfigurations = activity.IsChangingConfigurations;
            if (--activityReferences == 0 && !isActivityChangingConfigurations)
            {
                // App enters background
                MusicService.PauseMusic();
            }
        }
    }
}