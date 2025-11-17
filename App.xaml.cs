using gaddammd_ContactListApplication.ViewModels;

namespace gaddammd_ContactListApplication;

public partial class App : Application
{
	public static ContactsViewModel ContactsViewModel { get; set; } = null!;

	public App()
	{
		InitializeComponent();
		ContactsViewModel = new ContactsViewModel();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}
