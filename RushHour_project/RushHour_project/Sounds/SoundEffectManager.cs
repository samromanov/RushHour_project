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
using Android.Media;

namespace RushHour_project.Sounds
{
    public static class SoundEffectManager
    {
        private static SoundPool soundPool;
        private static Dictionary<string, int> soundMap = new Dictionary<string, int>();
        private static bool isLoaded = false;
        public static Action OnLoaded;

        [Obsolete]
        public static void Init(Context context)
        {
            if (soundPool == null)
            {
                soundPool = new SoundPool.Builder().SetMaxStreams(5).Build();
                soundPool.SetOnLoadCompleteListener(new SoundPoolLoadListener());
                // Preload your SFX here
                soundMap["click"] = soundPool.Load(context, Resource.Raw.btnClick, 1);
                soundMap["win"] = soundPool.Load(context, Resource.Raw.winState, 1);
                soundMap["loading"] = soundPool.Load(context, Resource.Raw.startup, 1);
                // Add more as needed
            }
        }

        public static void Play(string key)
        {
            if (isLoaded && soundMap.ContainsKey(key))
                soundPool.Play(soundMap[key], 1, 1, 1, 0, 1f);
        }

        private class SoundPoolLoadListener : Java.Lang.Object, SoundPool.IOnLoadCompleteListener
        {
            public void OnLoadComplete(SoundPool soundPool, int sampleId, int status)
            {
                isLoaded = true;
                OnLoaded?.Invoke();
            }
        }
    }
}