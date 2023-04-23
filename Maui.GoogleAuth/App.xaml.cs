using Maui.GoogleAuth.Views;

namespace Maui.GoogleAuth;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new SplashscreenPage();
	}
}
