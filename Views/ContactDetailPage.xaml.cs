using gaddammd_ContactListApplication.ViewModels;

namespace gaddammd_ContactListApplication.Views;

public partial class ContactDetailPage : ContentPage
{
	public ContactDetailPage()
	{
		InitializeComponent();
		BindingContext = App.ContactsViewModel;
	}
}
