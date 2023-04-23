using Maui.GoogleAuth.Services;

namespace Maui.GoogleAuth.Views;

public partial class SplashscreenPage : ContentPage
{
    private IGoogleAuthService _googleAuthService;
	public SplashscreenPage()
	{
		InitializeComponent();

        Loaded+= SplashscreenPage_Loaded;
	}

    private async void SplashscreenPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            _googleAuthService = new GoogleAuthService();

            await Task.Delay(1000);
            var currentUser = await _googleAuthService.GetCurrentUserAsync();
            if (currentUser == null)
                Application.Current.MainPage = new MainPage(_googleAuthService);
            else
                Application.Current.MainPage = new NavigationPage(new ProfilePage(_googleAuthService,currentUser));

        }
        catch (Exception)
        {
            Application.Current.MainPage = new MainPage(_googleAuthService);
        }
    }
}