using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using gaddammd_ContactListApplication.Models;
using Contact = gaddammd_ContactListApplication.Models.Contact;

namespace gaddammd_ContactListApplication.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private string? email;

    [ObservableProperty]
    private string? phoneNumber;

    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private ObservableCollection<Contact> contacts;

    [ObservableProperty]
    private Contact? selectedContact;

    public ContactsViewModel()
    {
        contacts = new ObservableCollection<Contact>();
    }

    [RelayCommand]
    public async Task SaveContact()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await Application.Current!.MainPage!.DisplayAlert("Validation Error", "Please enter a contact name.", "OK");
            return;
        }

        var contact = new Contact
        {
            Name = Name,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Description = Description
        };

        Contacts.Add(contact);
        ClearForm();

        // Navigate to Contacts Page using absolute routing
        await Shell.Current.GoToAsync("///contactspage");
    }

    [RelayCommand]
    public async Task SelectContact(Contact contact)
    {
        SelectedContact = contact;
        await Shell.Current.GoToAsync($"///contactdetail");
    }

    [RelayCommand]
    public async Task GoToAddContact()
    {
        ClearForm();
        await Shell.Current.GoToAsync("///addcontactpage");
    }

    [RelayCommand]
    public async Task GoToContactsList()
    {
        await Shell.Current.GoToAsync("///contactspage");
    }

    [RelayCommand]
    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    private void ClearForm()
    {
        Name = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        Description = string.Empty;
    }
}
