﻿using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RushHour_project.Classes;
using System.Linq;

namespace RushHour_project
{
    public class User
    {
        // listener
        private Firebase.Firestore.IListenerRegistration _usersListener;


        public string Uid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Firebase Authentication and Firestore
        FirebaseAuth firebaseAuthentication;
        FirebaseFirestore database;

        public const string USERS_COLLECTION = "users"; // the collection of the users in the database (collection)
        public const string CURRENT_USER_FILE = "currentUserFile"; // the current user that uses the application


        public User()
        {
            this.firebaseAuthentication = FirebaseHelper.GetFirebaseAuthentication();
            this.database = FirebaseHelper.GetFirestore();
        }
        public User(string email)
        {
            this.Email = email;
            this.firebaseAuthentication = FirebaseHelper.GetFirebaseAuthentication();
            this.database = FirebaseHelper.GetFirestore();
        }
        public User(string username, string email, string password) // when registering
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.firebaseAuthentication = FirebaseHelper.GetFirebaseAuthentication();
            this.database = FirebaseHelper.GetFirestore();
        }
        public User(string email, string password) // when logging in
        {
            this.Email = email;
            this.Password = password;
            this.firebaseAuthentication = FirebaseHelper.GetFirebaseAuthentication();
            this.database = FirebaseHelper.GetFirestore();
        }


        public static void SaveUserInfo_SP(string username, string email, string regDate, string userRecords_json, int last_level_index, bool isAdmin) // saves the current user info to the SharedPreference (password not saved)
        {
            // Get SharedPreferences Editor
            var editor = Application.Context.GetSharedPreferences(CURRENT_USER_FILE, FileCreationMode.Private).Edit();

            // Store username, email and password and more
            editor.PutString("username", username);
            editor.PutString("email", email);
            editor.PutString("regDate", regDate);
            editor.PutString("userRecords", userRecords_json);
            editor.PutBoolean("isLoggedIn", true);
            editor.PutInt("connected_last_level_index", last_level_index);
            editor.PutBoolean("isAdmin", isAdmin);

            // Apply changes
            editor.Apply();
        }

        public async Task<bool> GetUserByEmail()
        {
            try
            {
                var obj = await this.database.Collection(USERS_COLLECTION).WhereEqualTo("email", this.Email).Get();
                QuerySnapshot snapshot = (QuerySnapshot)obj;
                if (snapshot.Documents.Count > 0)
                {
                    DocumentSnapshot item = snapshot.Documents[0];
                    if (item.Get("email") != null)
                    {
                        this.Email = item.Get("email").ToString();
                    }
                    else
                    {
                        this.Email = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        public async Task<bool> Login()
        {
            try
            {
                // Authenticate user in Firebase Authentication
                await this.firebaseAuthentication.SignInWithEmailAndPassword(this.Email, this.Password);

                // Get the authenticated user's UID
                string uid = this.firebaseAuthentication.CurrentUser.Uid;
                this.Uid = uid;

                // Fetch the user document from Firestore using the UID
                DocumentReference userReference = this.database.Collection(USERS_COLLECTION).Document(uid);
                DocumentSnapshot documentSnapshot = (DocumentSnapshot)await userReference.Get();


                string regDate = DateTime.Now.ToString("dd/MM/yyyy"); // Default date if not found
                string username = "User"; // Default username if not found
                string userRecords_json;
                int connected_last_level_index = 0;
                bool isAdmin = false;
                //string lastLevelIndex_json;

                // Extract the regDate from the document
                // Extract the username from the document
                if (documentSnapshot.Exists())
                {
                    username = documentSnapshot.GetString("username");
                    regDate = documentSnapshot.GetString("regDate");
                }
                if (!documentSnapshot.Contains("userRecords")) // if the userRecords doesn't exist in the user's data base
                {
                    Dictionary<int, int?> userRecords = new Dictionary<int, int?>();
                    // Initialise with nulls for 50 levels
                    for (int i = 1; i <= 60; i++)
                        userRecords[i] = null;
                    userRecords_json = JsonConvert.SerializeObject(userRecords);
                    Dictionary<string, Java.Lang.Object> updateMap = new Dictionary<string, Java.Lang.Object>
                    {
                        { "userRecords", new Java.Lang.String(userRecords_json) }
                    };

                    await userReference.Set(updateMap, SetOptions.Merge());
                }
                else //if the userRecords exist in the user's data base
                {
                    userRecords_json = documentSnapshot.GetString("userRecords"); // get the userRecords_json from the user
                }
                if (!documentSnapshot.Contains("connected_last_level_index"))
                {
                    //lastLevelIndex_json = JsonConvert.SerializeObject(connected_last_level_index);
                    Dictionary<string, Java.Lang.Object> updateMap = new Dictionary<string, Java.Lang.Object>
                    {
                        { "connected_last_level_index", new Java.Lang.Integer(connected_last_level_index) }
                    };
                    await userReference.Set(updateMap, SetOptions.Merge());
                }
                else
                {
                    connected_last_level_index =(int)documentSnapshot.Get("connected_last_level_index"); // get the connected_last_level_index from the user
                }
                if (!documentSnapshot.Contains("isAdmin"))
                {
                    Dictionary<string, Java.Lang.Object> updateMap = new Dictionary<string, Java.Lang.Object>
                    {
                        { "isAdmin", new Java.Lang.Boolean(isAdmin) }
                    };
                    await userReference.Set(updateMap, SetOptions.Merge());
                }
                else
                {
                    isAdmin = (bool)documentSnapshot.GetBoolean("isAdmin"); // get the isAdmin from the user
                }

                // Display the welcome Toast message
                Toast.MakeText(Application.Context, $"Welcome, {username}!", ToastLength.Short).Show();

                User.SaveUserInfo_SP(username, this.Email, regDate, userRecords_json, connected_last_level_index, isAdmin);

                return true; 
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await Task.Run(() =>
                {
                    var editor = Application.Context.GetSharedPreferences(CURRENT_USER_FILE, FileCreationMode.Private).Edit();
                    editor.PutString("username", "");
                    editor.PutString("email", "");
                    editor.PutString("userRecords", "");
                    editor.PutBoolean("isLoggedIn", false);
                    editor.PutInt("connected_last_level_index", 0);
                    editor.PutBoolean("isAdmin", false);

                    editor.Apply(); // Still doesn't return a Task, but now it's executed on a background thread
                });

                // If firebaseAuthentication has an asynchronous sign-out method, use it:
                // await firebaseAuthentication.SignOutAsync();

                // Otherwise, if it's synchronous:
                firebaseAuthentication.SignOut();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Register(bool isAdmin = false)
        {
            try
            {
                // Create user in Firebase Authentication
                await firebaseAuthentication.CreateUserWithEmailAndPassword(this.Email, this.Password);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            try
            {
                string registrationDate = DateTime.Now.ToString("dd/MM/yyyy"); //the date of the user's registration
                int connected_last_level_index = 0;

                Dictionary<int, int?> userRecords = new Dictionary<int, int?>();

                // Initialise with nulls for 50 levels
                for (int i = 1; i <= 60; i++)
                    userRecords[i] = null;
                string userRecords_json = JsonConvert.SerializeObject(userRecords);

                HashMap userMap = new HashMap();
                userMap.Put("username", this.Username);
                userMap.Put("email", this.Email);
                userMap.Put("isAdmin", isAdmin);
                userMap.Put("regDate", registrationDate);
                userMap.Put("userRecords", userRecords_json);
                userMap.Put("connected_last_level_index", connected_last_level_index);

                // Save only username and email to SharedPreferences
                User.SaveUserInfo_SP(this.Username, this.Email, registrationDate, userRecords_json, connected_last_level_index, isAdmin);

                DocumentReference userReference = this.database.Collection(USERS_COLLECTION).Document(this.firebaseAuthentication.CurrentUser.Uid);
                await userReference.Set(userMap);

                // Fetch the user document from Firestore using the UID
                DocumentSnapshot documentSnapshot = (DocumentSnapshot)await userReference.Get();

                string username = "User"; // Default username if not found

                // Extract the username from the document
                if (documentSnapshot.Exists())
                {
                    username = documentSnapshot.GetString("username");
                }

                // Display the welcome Toast message
                Toast.MakeText(Application.Context, $"Welcome, {username}!", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                firebaseAuthentication.CurrentUser?.Delete();
                return false;
            }
            return true;
        }
        public async Task<Dictionary<int,int?>> FetchRecordsDict()
        {
            try
            {
                // Fetch the user document from Firestore using the UID
                DocumentReference userReference = this.database.Collection(USERS_COLLECTION).Document(this.Uid);
                DocumentSnapshot documentSnapshot = (DocumentSnapshot)await userReference.Get();

                string records_json = documentSnapshot.GetString("userRecords");

                // Deserialise
                Dictionary<int, int?> restoredRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(records_json);
                return restoredRecords;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
            }
            return null;
        }
        //public async Task<int> CountUserDoneLevels()
        //{
        //    try
        //    {
        //        // Fetch the user document from Firestore using the UID
        //        DocumentReference userReference = this.database.Collection(USERS_COLLECTION).Document(this.Uid);
        //        DocumentSnapshot documentSnapshot = (DocumentSnapshot)await userReference.Get();

        //        string userRecords_json = documentSnapshot.GetString("userRecords");

        //        Dictionary<int,int?> userRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(userRecords_json);
        //        int levelsDoneCount = 0;
        //        foreach (var kvp in userRecords)
        //        {
        //            if (kvp.Value != null)
        //            {
        //                levelsDoneCount++;
        //            }
        //        }
        //        return levelsDoneCount;
        //    }
        //    catch (Exception ex)
        //    {
        //        Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
        //    }
        //    return 0;
        //}
        //public async Task<int> TotalPointsSum()
        //{
        //    try
        //    {
        //        // Fetch the user document from Firestore using the UID
        //        DocumentReference userReference = this.database.Collection(USERS_COLLECTION).Document(this.Uid);
        //        DocumentSnapshot documentSnapshot = (DocumentSnapshot)await userReference.Get();

        //        string userRecords_json = documentSnapshot.GetString("userRecords");

        //        Dictionary<int, int?> userRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(userRecords_json);
        //        int totalPointsSum = 0;
        //        foreach (var kvp in userRecords)
        //        {
        //            if (kvp.Value != null)
        //            {
        //                totalPointsSum += int.Parse(kvp.Value.ToString());
        //            }
        //        }
        //        return totalPointsSum;
        //    }
        //    catch (Exception ex)
        //    {
        //        Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
        //    }
        //    return 0;
        //}

        public async Task<List<UserRecords>> GetSortedLeaderboard()
        {
            var users = await new User().GetAllUsersRecordsList();

            //var sorted = users.OrderByDescending(u => u.Records.Count) // Most levels done
            //                  .ThenBy(u => u.Records.Values.Sum(v => v ?? int.MaxValue)) // Least total steps
            //                  .ToList();
            var sorted = users.OrderByDescending(u => u.Points).ToList();


            return sorted;
        }

        //updates the user's records dictionary as string in the firebase database
        public async void UpdateUserRecords(Dictionary<int,int?> dict)
        {
            try
            {
                //string userRecords_json = JsonConvert.SerializeObject(dict);
                //DocumentReference userReference = this.database.Collection(USERS_COLLECTION).Document(this.firebaseAuthentication.CurrentUser.Uid);

                //await userReference.Update("userRecords", userRecords_json);

                string userRecords_json = JsonConvert.SerializeObject(dict);
                DocumentReference userReference = this.database.Collection(USERS_COLLECTION).Document(this.firebaseAuthentication.CurrentUser.Uid);

                Dictionary<string, Java.Lang.Object> updateMap = new Dictionary<string, Java.Lang.Object>
                {
                    { "userRecords", new Java.Lang.String(userRecords_json) }
                };

                await userReference.Set(updateMap, SetOptions.Merge());

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
            }
        }


        // the function runs through every user in the users collection,
        // get his records and name and email, and adding each user's records to a list and returns it
        public async Task<List<UserRecords>> GetAllUsersRecordsList()
        {
            try
            {
                var usersRef = FirebaseFirestore.Instance.Collection("users");
                var snapshot = await usersRef.Get().AsAsync<QuerySnapshot>();

                List<UserRecords> allUsersRecordsList = new List<UserRecords>();

                foreach (var document in snapshot.Documents)
                {
                    var documentData = document.Data;
                    string uid = document.Id; // this is the document ID (the user UID)

                    // For example:
                    string email = document.Contains("email") ? document.GetString("email") : "No email found";
                    string username = document.Contains("username") ? document.GetString("username") : "No username found";
                    string userRecordsJson = document.Contains("userRecords") ? document.GetString("userRecords") : null;

                    Dictionary<int, int?> userRecords = new Dictionary<int, int?>();
                    if (userRecordsJson != null)
                        userRecords = JsonConvert.DeserializeObject<Dictionary<int, int?>>(userRecordsJson);

                    int doneLevelsAmount = CountUserDoneLevels(userRecords);
                    int totalStepsSum = TotalPointsSum(userRecords);

                    int points = (doneLevelsAmount * 1000) - totalStepsSum;

                    if (points <= 0)
                    {
                        points = 0;
                    }

                    UserRecords records = new UserRecords(username, email, userRecords, points);

                    allUsersRecordsList.Add(records);


                }
                return allUsersRecordsList;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error {ex.Message}", ToastLength.Short).Show();
            }
            return new List<UserRecords>();
        }
        public static int CountUserDoneLevels(Dictionary<int, int?> records)
        {
            return records.Count(r => r.Value.HasValue);
        }

        public static int TotalPointsSum(Dictionary<int, int?> records)
        {
            if (records == null || records.Count == 0)
                return 0;
            return records.Values.Where(v => v.HasValue).Sum(v => v.Value);
        }
        public IListenerRegistration ListenToLeaderboardUpdates(Action<List<UserRecords>> onLeaderboardChanged)
        {
            var usersRef = FirebaseFirestore.Instance.Collection("users");
            return usersRef.AddSnapshotListener(new LeaderboardEventListener(onLeaderboardChanged));
        }
    }
}