using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Storage;
using Xamarin.Essentials;
using static Java.Text.Normalizer;

namespace RushHour_project
{
    public static class FirebaseHelper // connects the project to the firebase database
    {
        //Set project_id and api_key from firebase json file(downlowded from firebase console);
        const string PROJECT_ID = "rushhour-project";
        const string API_KEY = "AIzaSyBzPEG7-zrvqGT1P-3HYe7j2OxKLeR-cds";
        const string PROJECT_NAME = "RushHour-project";

        static FirebaseFirestore database;
        public static FirebaseFirestore GetFirestore()
        {
            if (database != null)
            {
                return database;
            }
            var app = FirebaseApp.InitializeApp(Application.Context);

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId(PROJECT_ID)
                    .SetApplicationId(PROJECT_ID)
                    .SetApiKey(API_KEY)
                    .SetDatabaseUrl($"https://{PROJECT_ID}.firebaseio.com")
                    .SetStorageBucket($"{PROJECT_ID}.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options, PROJECT_NAME);
                database = FirebaseFirestore.GetInstance(app);
            }
            else
            {
                database = FirebaseFirestore.GetInstance(app);
            }
            return database;
        }
        public static FirebaseAuth GetFirebaseAuthentication()
        {
            FirebaseAuth firebaseAuthentication;
            var app = FirebaseApp.InitializeApp(Application.Context);
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId(PROJECT_ID)
                    .SetApplicationId(PROJECT_ID)
                    .SetApiKey(API_KEY)
                    .SetDatabaseUrl($"https://{PROJECT_ID}.firebaseio.com")
                    .SetStorageBucket($"{PROJECT_ID}.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(Application.Context, options);
                firebaseAuthentication = FirebaseAuth.Instance;
            }
            else
            {
                firebaseAuthentication = FirebaseAuth.Instance;
            }
            return firebaseAuthentication;
        }

        public static FirebaseStorage GetFirebaseStorage()
        {
            FirebaseStorage firebaseStorage;
            var app = FirebaseApp.InitializeApp(Application.Context);
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId(PROJECT_ID)
                    .SetApplicationId(PROJECT_ID)
                    .SetApiKey(API_KEY)
                    .SetDatabaseUrl($"https://{PROJECT_ID}.firebaseio.com")
                    .SetStorageBucket($"{PROJECT_ID}.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(Application.Context, options);
                firebaseStorage = FirebaseStorage.Instance;
            }
            else
            {
                firebaseStorage = FirebaseStorage.Instance;
            }
            return firebaseStorage;
        }
    }
}