using Maui.GoogleAuth.Services;
using Maui.GoogleAuth.Views;

namespace Maui.GoogleAuth;

public partial class MainPage : ContentPage
{
	int count = 0;
	private IGoogleAuthService _googleAuthService;
	public MainPage(IGoogleAuthService _googleAuthService)
	{
		InitializeComponent();
    }


    protected override void OnAppearing()
    {
        _googleAuthService = new GoogleAuthService();
    }
    private async void OnCounterClicked(object sender, EventArgs e)
	{
		var currentUser = await _googleAuthService.AuthenticateAsync();

		if(currentUser != null)
            Application.Current.MainPage = new NavigationPage(new ProfilePage(_googleAuthService, currentUser));

    }
}

