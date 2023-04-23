using Maui.GoogleAuth.Services;

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
		var user = await _googleAuthService.AuthenticateAsync();

		if(user != null)
		{
			IdLbl.Text = "ID - "+ user.Id;
			EmailLbl.Text = "Email - " + user.Email;
			UsernameLbl.Text = "Name - " + user.FullName;
		}
	}
}

