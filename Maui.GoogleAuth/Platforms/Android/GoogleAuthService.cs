using Android.App;
using Android.Gms.Auth.Api.SignIn;
using Maui.GoogleAuth.Models;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.GoogleAuth.Services
{
    public partial class GoogleAuthService
    {
        const int RC_SIGN_IN = 9001;

        /// <summary>
        /// Current activity
        /// </summary>
        private Android.App.Activity _activity;

        /// <summary>
        /// Google Sign in options
        /// </summary>
        private GoogleSignInOptions _gso;

        /// <summary>
        /// Google Sign in client
        /// </summary>
        private GoogleSignInClient _googleSignInClient;


        private TaskCompletionSource<UserDTO> _taskCompletionSource;
        private Task<UserDTO> GoogleAuthentication
        {
            get => _taskCompletionSource.Task;
        }
        public GoogleAuthService()
        {
            // Get current activity
            _activity = Platform.CurrentActivity;

            // Config Auth Option
            _gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                    //.RequestServerAuthCode("550657561963-7iov7dcp0hu034r29q6m3jrpi1pcnj4v.apps.googleusercontent.com")
                    .RequestIdToken("550657561963-gu6o6a0sn797hunbp72p8051vbi7n641.apps.googleusercontent.com")
                    .RequestEmail()
                    .RequestId()
                    .RequestProfile()
                   //.RequestIdToken("") If we need to link with firebase auth
                   .Build();

            // Get client
            _googleSignInClient = GoogleSignIn.GetClient(_activity, _gso);


            MainActivity.ResultGoogleAuth += MainActivity_ResultGoogleAuth;
        }
        public Task LogoutAsync() => _googleSignInClient.SignOutAsync();
        public async Task<UserDTO> GetCurrentUserAsync()
        {
            try
            {
                var user = await _googleSignInClient.SilentSignInAsync();

                return new UserDTO
                {
                    Email = user.Email,
                    FullName = $"{user.DisplayName}",
                    Id = user.Id,
                    UserName = user.GivenName
                };

            }
            catch (Exception)
            {
                throw new Exception("Error");
            }

        }
        private void MainActivity_ResultGoogleAuth(object sender, (bool Success, GoogleSignInAccount Account) e)
        {
            if(_taskCompletionSource != null)
                if(_taskCompletionSource?.Task.Status == TaskStatus.RanToCompletion)
                    _taskCompletionSource = new TaskCompletionSource<UserDTO>();
            
            if (e.Success)
                _taskCompletionSource?.SetResult(new UserDTO
                {
                    Email = e.Account.Email,
                    FullName = $"{e.Account.DisplayName}",
                    Id = e.Account.Id,
                    UserName = e.Account.GivenName
                });
            else
                _taskCompletionSource?.SetException(new Exception("Error"));
        }

        public Task<UserDTO> AuthenticateAsync()
        {
            _taskCompletionSource = new TaskCompletionSource<UserDTO>();

            _activity.StartActivityForResult(_googleSignInClient.SignInIntent, RC_SIGN_IN);

            return GoogleAuthentication;
        }
    }
}
