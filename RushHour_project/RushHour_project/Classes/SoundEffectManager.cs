using Android.Content;
using Android.Media;
using System.Collections.Generic;
using System; // Make sure System is included for Action

namespace RushHour_project.Classes
{
    public static class SoundEffectManager
    {
        private static SoundPool soundPool;
        private static Dictionary<string, int> soundMap = new Dictionary<string, int>();
        private static bool isLoaded = false;
        private static int soundsToLoadCount; // Total number of sounds expected to load
        private static int soundsLoadedCount; // Counter for sounds that have successfully loaded

        public static Action OnLoaded; // Callback for when all sounds are loaded

        public static void Init(Context context)
        {
            if (soundPool == null)
            {
                // Ensure SoundPool is initialized for API level 21+
                soundPool = new SoundPool.Builder().SetMaxStreams(5).Build();
                soundPool.SetOnLoadCompleteListener(new SoundPoolLoadListener());

                soundsToLoadCount = 0; // Reset count for each Init call
                soundsLoadedCount = 0; // Reset loaded count

                // Load your SFX here
                // IMPORTANT: Increment soundsToLoadCount for EACH sound you load
                soundMap["click"] = soundPool.Load(context, Resource.Raw.click, 1);
                soundsToLoadCount++;
                soundMap["win"] = soundPool.Load(context, Resource.Raw.win, 1);
                soundsToLoadCount++;
                soundMap["loading"] = soundPool.Load(context, Resource.Raw.loading, 1);
                soundsToLoadCount++;
                // Add more as needed
            }
        }

        public static void Play(string key)
        {
            // This method can be called directly, but for initial splash, use OnLoaded callback
            if (isLoaded && soundMap.ContainsKey(key))
            {
                soundPool.Play(soundMap[key], 1, 1, 1, 0, 1f);
            }
        }

        public static void Release()
        {
            if (soundPool != null)
            {
                try
                {
                    soundPool.Release();
                }
                catch (Exception ex)
                {
                    // Log the exception if needed, but don't crash the app
                    System.Diagnostics.Debug.WriteLine($"Error releasing SoundPool: {ex.Message}");
                }
                finally
                {
                    soundPool = null;
                    soundMap.Clear();
                    isLoaded = false;
                    OnLoaded = null; // Clear callback
                    soundsToLoadCount = 0;
                    soundsLoadedCount = 0;
                }
            }
        }

        private class SoundPoolLoadListener : Java.Lang.Object, SoundPool.IOnLoadCompleteListener
        {
            public void OnLoadComplete(SoundPool soundPool, int sampleId, int status)
            {
                if (status == 0) // Status 0 indicates successful load
                {
                    soundsLoadedCount++;
                    if (soundsLoadedCount >= soundsToLoadCount)
                    {
                        // All expected sounds are now loaded
                        isLoaded = true;
                        OnLoaded?.Invoke(); // Invoke the callback
                    }
                }
                else
                {
                    // Handle load error (e.g., log it)
                    System.Diagnostics.Debug.WriteLine($"Error loading sound with sampleId: {sampleId}, status: {status}");
                }
            }
        }
    }
} 