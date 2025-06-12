using System;

public class MusicService
{
    private static MediaPlayer mediaPlayer;
    private static int currentResId = -1;

    public static void StopMusic()
    {
        if (mediaPlayer != null)
        {
            try
            {
                mediaPlayer.Stop();
                mediaPlayer.Release();
            }
            catch (Exception ex)
            {
                // Log the exception if needed, but don't crash the app
                System.Diagnostics.Debug.WriteLine($"Error stopping/releasing MediaPlayer: {ex.Message}");
            }
            finally
            {
                mediaPlayer = null;
                currentResId = -1;
            }
        }
    }

    public override void OnDestroy()
    {
        StopMusic();
    }
} 