using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RushHour_project.Sounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers; // For shake detection debouncing

namespace RushHour_project.Classes
{


    public class ShakeToPlayDialog : Dialog, ISensorEventListener
    {
        private Context _context;
        private Action<int> _onLevelSelected;
        private TextView _messageTextView;
        private TextView _levelChosenTextView;
        private Button _startLevelButton;

        // Sensor related fields
        private SensorManager _sensorManager;
        private Sensor _accelerometer;
        private float _lastX, _lastY, _lastZ;
        private long _lastUpdate;
        private const int SHAKE_THRESHOLD = 40; // Increased sensitivity further
        private const int SHAKE_DEBOUNCE_TIME = 2000; // milliseconds
        private bool _shakeDetected = false;
        private bool _isFirstSensorReading = true; // New flag
        private System.Timers.Timer _shakeDebounceTimer;
        private int _chosenLevel = -1;
        private Random _random;

        public ShakeToPlayDialog(Context context, Action<int> onLevelSelected) : base(context)
        {
            _context = context;
            _onLevelSelected = onLevelSelected;
            _random = new Random();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle); // Corrected: Cast to int
            SetContentView(Resource.Layout.dialog_shake_to_play);

            _messageTextView = FindViewById<TextView>(Resource.Id.messageTextView);
            _levelChosenTextView = FindViewById<TextView>(Resource.Id.levelChosenTextView);
            _startLevelButton = FindViewById<Button>(Resource.Id.startLevelButton);

            _startLevelButton.Click += (sender, e) =>
            {
                if (_chosenLevel != -1)
                {
                    _onLevelSelected?.Invoke(_chosenLevel);
                    Dismiss(); // Close the dialog
                    }
            };

            // Initialize sensor manager
            _sensorManager = (SensorManager)_context.GetSystemService(Context.SensorService);
            _accelerometer = _sensorManager.GetDefaultSensor(SensorType.Accelerometer);

            // Set up debounce timer
            _shakeDebounceTimer = new System.Timers.Timer(SHAKE_DEBOUNCE_TIME);
            _shakeDebounceTimer.Elapsed += (s, e) => _shakeDetected = false;
            _shakeDebounceTimer.AutoReset = false; // Only run once per interval

            // Register listener when dialog starts
            RegisterSensorListener();
        }

        // --- ISensorEventListener Implementation ---
        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // Do nothing for now
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.Accelerometer)
            {
                if (_isFirstSensorReading)
                {
                    _lastX = e.Values[0];
                    _lastY = e.Values[1];
                    _lastZ = e.Values[2];
                    _lastUpdate = System.Environment.TickCount;
                    _isFirstSensorReading = false;
                    return; // Ignore this first reading for shake detection
                }

                long curTime = System.Environment.TickCount; // Current time in milliseconds
                if ((curTime - _lastUpdate) > 100) // Only check every 100ms for performance
                {
                    long diffTime = (curTime - _lastUpdate);
                    _lastUpdate = curTime;

                    float x = e.Values[0];
                    float y = e.Values[1];
                    float z = e.Values[2];

                    // New: Calculate speed based on sum of absolute differences
                    float speed = Math.Abs(x - _lastX) + Math.Abs(y - _lastY) + Math.Abs(z - _lastZ);

                    if (speed > SHAKE_THRESHOLD && !_shakeDetected)
                    {
                        _shakeDetected = true;
                        _shakeDebounceTimer.Start(); // Start debounce timer

                        // Shake detected!
                        UnregisterSensorListener(); // Stop listening for shakes once detected

                        // Choose a random level (e.g., between 1 and 60, adjust as per your game levels)
                        _chosenLevel = _random.Next(1, 61); // 1 to 60 inclusive

                        // Update UI on the main thread
                        (_context as Activity)?.RunOnUiThread(() =>
                        {
                            _messageTextView.Visibility = ViewStates.Gone;
                            _levelChosenTextView.Text = $"Level {_chosenLevel} chosen!";
                            _levelChosenTextView.Visibility = ViewStates.Visible;
                            _startLevelButton.Enabled = true; // Enable the button
                        });
                        SoundEffectManager.Play("win"); // Play a sound effect for choosing a level
                    }

                    _lastX = x;
                    _lastY = y;
                    _lastZ = z;
                }
            }
        }
        // --- End ISensorEventListener Implementation ---

        private void RegisterSensorListener()
        {
            if (_sensorManager != null && _accelerometer != null)
            {
                _sensorManager.RegisterListener(this, _accelerometer, SensorDelay.Game); // Use SensorDelay.Game for responsive updates
                _lastUpdate = System.Environment.TickCount;
                _isFirstSensorReading = true; // Reset flag when re-registering
                _lastX = _lastY = _lastZ = 0; // Explicitly zero out for a fresh start
            }
        }

        private void UnregisterSensorListener()
        {
            if (_sensorManager != null)
            {
                _sensorManager.UnregisterListener(this);
                // This is now redundant as it's done in RegisterSensorListener for consistency
                // _lastX = _lastY = _lastZ = 0;
            }
        }

        // Ensure sensor listener is unregistered when dialog is dismissed
        public override void Dismiss()
        {
            UnregisterSensorListener();
            _shakeDebounceTimer?.Stop();
            _shakeDebounceTimer?.Dispose();
            base.Dismiss();
        }

        // Called when the dialog is explicitly cancelled (e.g., by back button)
        public override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            // This is important for clean up if dialog is dismissed in other ways
            UnregisterSensorListener();
            _shakeDebounceTimer?.Stop();
            _shakeDebounceTimer?.Dispose();
        }
    }
}