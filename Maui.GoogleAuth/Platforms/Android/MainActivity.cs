using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api.SignIn;
using Android.OS;
using Android.Widget;
using Google.Android.Material.Snackbar;

namespace Maui.GoogleAuth;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{

    public static event EventHandler<(bool Success, GoogleSignInAccount Account)> ResultGoogleAuth;
    public static Activity BaseActivity { get; set; }
    protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        if (requestCode == 9001)
        {
            try
            {
                var currentAccount = await GoogleSignIn.GetSignedInAccountFromIntentAsync(data);
                ResultGoogleAuth?.Invoke(this, (currentAccount.Email != null, currentAccount));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, $"Error - {ex.Message}", ToastLength.Long).Show();

                ResultGoogleAuth?.Invoke(this, (false, null));
            }
        }
    }
}
