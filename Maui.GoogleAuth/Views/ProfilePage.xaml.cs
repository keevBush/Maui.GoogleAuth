using Maui.GoogleAuth.Services;

namespace Maui.GoogleAuth.Views;

public partial class ProfilePage : ContentPage
{
	private IGoogleAuthService _googleAuthService;
	public ProfilePage(IGoogleAuthService googleAuthService, Models.UserDTO currentUser)
	{
		InitializeComponent();

		_googleAuthService = googleAuthService;

		spID.Text = currentUser.Id;
		spEmail.Text = currentUser.Email;
		spName.Text = currentUser.FullName;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await _googleAuthService.LogoutAsync();
		await Task.Delay(1000);
		Application.Current.MainPage = new MainPage(_googleAuthService);
    }
}