using gaddammd_ContactListApplication.ViewModels;

namespace gaddammd_ContactListApplication.Views;

public partial class AddContactPage : ContentPage
{
	public AddContactPage()
	{
		InitializeComponent();
		BindingContext = App.ContactsViewModel;
	}
}
