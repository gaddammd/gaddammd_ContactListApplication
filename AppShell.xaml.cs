using gaddammd_ContactListApplication.Views;

namespace gaddammd_ContactListApplication;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("contactdetail", typeof(ContactDetailPage));
	}
}
