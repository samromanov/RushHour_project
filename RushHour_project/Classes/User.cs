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

        public const string COLLECTION_NAME = "users"; // the collection of the users in the database (collection)
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


        public static void SaveUserInfo(string username, string email, string password) // saves the current user info to the SharedPreference
        {
            // Get SharedPreferences Editor
            var editor = Application.Context.GetSharedPreferences(CURRENT_USER_FILE, FileCreationMode.Private).Edit();

            // Store username, email and password
            editor.PutString("username", username);
            editor.PutString("email", email);
            editor.PutString("password", password);

            // Apply changes
            editor.Apply();
        }

        public async Task<bool> GetUserByEmil()
        {
            try
            {
                var obj = await this.database.Collection(COLLECTION_NAME).WhereEqualTo("email", this.Email).Get();
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
                await this.firebaseAuthentication.SignInWithEmailAndPassword(this.Email, this.Password);
                this.Uid = firebaseAuthentication.Uid;
                User.SaveUserInfo(this.Username, this.Email, this.Password);

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            return true;
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
                    editor.PutString("password", "");
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
                //userMap.Put("password", this.Password);
                userMap.Put("isAdmin", isAdmin);

                DocumentReference userReference = this.database.Collection(COLLECTION_NAME).Document(this.firebaseAuthentication.CurrentUser.Uid);
                // creates me the user with the same Uid as in the firebase authentication
                await userReference.Set(userMap);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        
        
    }
}