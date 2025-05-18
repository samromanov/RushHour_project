using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RushHour_project
{
    public class User
    {
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


        public static void SaveUserInfo_SP(string username, string email) // saves the current user info to the SharedPreference (password not saved)
        {
            // Get SharedPreferences Editor
            var editor = Application.Context.GetSharedPreferences(CURRENT_USER_FILE, FileCreationMode.Private).Edit();

            // Store username, email and password
            editor.PutString("username", username);
            editor.PutString("email", email);

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

                string username = "User"; // Default username if not found

                // Extract the username from the document
                if (documentSnapshot.Exists())
                {
                    username = documentSnapshot.GetString("username");
                }

                // Display the welcome Toast message
                Toast.MakeText(Application.Context, $"Welcome, {username}!", ToastLength.Short).Show();

                User.SaveUserInfo_SP(username, this.Email);

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
                HashMap userMap = new HashMap();
                userMap.Put("username", this.Username);
                userMap.Put("email", this.Email);
                userMap.Put("isAdmin", isAdmin);

                // Save only username and email to SharedPreferences
                User.SaveUserInfo_SP(this.Username, this.Email);

                DocumentReference userReference = this.database.Collection(USERS_COLLECTION).Document(this.firebaseAuthentication.CurrentUser.Uid);
                await userReference.Set(userMap);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                firebaseAuthentication.CurrentUser?.Delete();
                return false;
            }
            return true;
        }
    }
}