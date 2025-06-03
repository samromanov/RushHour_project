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
using Android.Media;

namespace RushHour_project
{
    [Service]
    public class MusicService : Service
    {
        private static MediaPlayer mediaPlayer;
        private static int currentResId = -1;

        public override IBinder OnBind(Intent intent) => null;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            int resId = intent.GetIntExtra("MusicResId", -1);
            if (resId != -1 && resId != currentResId)
            {
                PlayMusic(resId);
            }
            return StartCommandResult.Sticky;
        }

        private void PlayMusic(int resId)
        {
            if (mediaPlayer != null)
            {
                mediaPlayer.Stop();
                mediaPlayer.Release();
                mediaPlayer = null;
            }
            mediaPlayer = MediaPlayer.Create(this, resId);
            mediaPlayer.Looping = true;
            mediaPlayer.Start();
            currentResId = resId;
        }

        public static void PauseMusic()
        {
            if (mediaPlayer != null && mediaPlayer.IsPlaying)
                mediaPlayer.Pause();
        }

        public static void ResumeMusic()
        {
            if (mediaPlayer != null && !mediaPlayer.IsPlaying)
                mediaPlayer.Start();
        }

        public static void StopMusic()
        {
            if (mediaPlayer != null)
            {
                mediaPlayer.Stop();
                mediaPlayer.Release();
                mediaPlayer = null;
                currentResId = -1;
            }
        }

        public override void OnDestroy()
        {
            StopMusic();
            base.OnDestroy();
        }
    }
}