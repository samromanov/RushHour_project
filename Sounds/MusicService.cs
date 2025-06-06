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
using RushHour_project.Sounds;

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
            if (intent?.Action == "PLAY_SFX")
            {
                int sfxResId = intent.GetIntExtra("SfxResId", -1);
                if (sfxResId != -1)
                    PlaySoundEffect(sfxResId);
                return StartCommandResult.NotSticky;
            }
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
        public void PlaySoundEffect(int sfxResId)
        {
            MediaPlayer sfxPlayer = MediaPlayer.Create(this, sfxResId);
            sfxPlayer.Looping = false;
            sfxPlayer.Start();

            // Release resources when done
            sfxPlayer.Completion += (sender, e) =>
            {
                sfxPlayer.Release();
                sfxPlayer.Dispose();
            };
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            try
            {
                //StopMusic();
                if (mediaPlayer != null)
                {
                    if (mediaPlayer.IsPlaying)
                        mediaPlayer.Stop();
                    mediaPlayer.Release();
                    mediaPlayer = null;
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
            }
        }
    }
}