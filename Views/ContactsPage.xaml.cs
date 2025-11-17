using gaddammd_ContactListApplication.ViewModels;

namespace gaddammd_ContactListApplication.Views;

public partial class ContactsPage : ContentPage
{
	public ContactsPage()
	{
		InitializeComponent();
		BindingContext = App.ContactsViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		// ViewModel persists across navigation in the same shell instance
	}
}
