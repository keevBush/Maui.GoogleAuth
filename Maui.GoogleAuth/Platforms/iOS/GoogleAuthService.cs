using Foundation;
using Google.SignIn;
using Maui.GoogleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace Maui.GoogleAuth.Services
{
    public partial class GoogleAuthService
    {
        #region Constantes
        private const string GoogleClientID = "XXX-XXX.apps.googleusercontent.com";
        #endregion
        #region Tasks
        private TaskCompletionSource<UserDTO> _taskCompletionSource;
        #endregion
        public Task LogoutAsync() => Task.FromResult(() => SignIn.SharedInstance.SignOutUser());
        public GoogleAuthService()
        {
            Google.SignIn.SignIn.SharedInstance.Scopes = new string[] { "https://www.googleapis.com/auth/userinfo.email" };
            Google.SignIn.SignIn.SharedInstance.ClientId = GoogleClientID;
        }
        public async Task<UserDTO> AuthenticateAsync()
        {
            _taskCompletionSource = new TaskCompletionSource<UserDTO>();

            Google.SignIn.SignIn.SharedInstance.SignedIn += SharedInstance_SignedIn;
            Google.SignIn.SignIn.SharedInstance.Scopes = new string[] { "https://www.googleapis.com/auth/userinfo.email" };
            Google.SignIn.SignIn.SharedInstance.ClientId = GoogleClientID;

            PreparePresentedViewController();
            
            Google.SignIn.SignIn.SharedInstance.SignInUser();


            return await _taskCompletionSource.Task;
        }

        private void PreparePresentedViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;

            var viewController = window.RootViewController;
            while(viewController.PresentingViewController != null)
            {
                viewController = viewController.PresentingViewController; ;
            }

            SignIn.SharedInstance.PresentingViewController = viewController;
        }

        private void SharedInstance_SignedIn(object sender, Google.SignIn.SignInDelegateEventArgs arg)
        {
            if (arg.Error != null)
            {
                _taskCompletionSource.TrySetException(new Exception($"Error - {arg.Error.LocalizedDescription} - {Convert.ToInt32(arg.Error.Code)}"));
                return;
            }

            var token = "";
            SignIn.SharedInstance.CurrentUser.Authentication.GetTokens((Authentication auth, NSError error) =>
            {
                if (error == null)
                    token = auth.IdToken;
                else
                {
                    _taskCompletionSource.TrySetException(new Exception($"Cannot get token id ERR -> {error.Code}  - {error.LocalizedDescription}"));
                    return;
                }
            });

            _taskCompletionSource.TrySetResult(new UserDTO
            {
                Id = token,
                Email = arg.User.Profile.Email,
                FullName = arg.User.Profile.Name,
                UserName = arg.User.Profile.Email,
            });
        }

        public async Task<UserDTO> GetCurrentUserAsync()
        {
            if(SignIn.SharedInstance.HasPreviousSignIn)
                SignIn.SharedInstance.RestorePreviousSignIn();

            var currentUser = SignIn.SharedInstance.CurrentUser;

            if (currentUser == null)
                throw new Exception("User not found");

            var token = "";

            return new UserDTO
            {
                Email = currentUser.Profile.Email,
                FullName = currentUser.Profile.Name,
                Id = currentUser.UserId,
                UserName = currentUser.Profile.Name
            };
        }

        [Export("signIn:didDisconnectWithUser:withError:")]
        public void DidDisconnect(SignIn signIn, GoogleUser user, NSError error)
        {

        }
    }
}
