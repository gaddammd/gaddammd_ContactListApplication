# Contact List Application - Implementation Guide

## Quick Reference for Key Features

### 1. Observable Properties (ViewModel)

The `[ObservableProperty]` attribute automatically generates change notification properties:

```csharp
[ObservableProperty]
private string? name;  // Automatically provides Name property with INotifyPropertyChanged

[ObservableProperty]
private ObservableCollection<Contact> contacts;  // Automatically notifies UI of changes
```

### 2. Relay Commands (ViewModel)

The `[RelayCommand]` attribute automatically generates RelayCommand instances:

```csharp
[RelayCommand]
public async Task SaveContact()
{
    // SaveContactCommand is automatically created
    // Can be used as: Command="{Binding SaveContactCommand}"
}

[RelayCommand]
public async Task SelectContact(Contact contact)
{
    // SelectContactCommand is automatically created with parameter support
}
```

### 3. Data Binding Examples (XAML)

**Two-Way Binding for Forms:**
```xaml
<Entry Text="{Binding Name}" />
<Editor Text="{Binding Description}" />
```

**Command Binding:**
```xaml
<Button Text="Save Contact" Command="{Binding SaveContactCommand}" />
```

**Command with Parameter:**
```xaml
<Button Command="{Binding SelectContactCommand}" 
        CommandParameter="{Binding .}" />
```

**Collection Binding:**
```xaml
<CollectionView ItemsSource="{Binding Contacts}"
                SelectionMode="Single"
                SelectionChangedCommand="{Binding SelectContactCommand}"
                SelectionChangedCommandParameter="{Binding SelectedItem, Source={RelativeSource Self}}">
```

### 4. Navigation Examples (C# Code)

**Navigate to Page:**
```csharp
await Shell.Current.GoToAsync("contactspage");
```

**Navigate with Parameters:**
```csharp
await Shell.Current.GoToAsync($"contactdetail?name={Uri.EscapeDataString(name)}");
```

**Navigate Back:**
```csharp
await Shell.Current.GoToAsync("..");
```

### 5. Validation Example

```csharp
[RelayCommand]
public async Task SaveContact()
{
    if (string.IsNullOrWhiteSpace(Name))
    {
        await Application.Current!.MainPage!.DisplayAlert("Validation Error", "Please enter a contact name.", "OK");
        return;
    }
    
    // Proceed with save
    var contact = new Contact
    {
        Name = Name,
        Email = Email,
        PhoneNumber = PhoneNumber,
        Description = Description
    };
    
    Contacts.Add(contact);  // ObservableCollection automatically notifies UI
}
```

### 6. Shared ViewModel Pattern

**In App.xaml.cs:**
```csharp
public static ContactsViewModel ContactsViewModel { get; set; } = null!;

public App()
{
    InitializeComponent();
    ContactsViewModel = new ContactsViewModel();  // Create once
}
```

**In Pages:**
```csharp
public AddContactPage()
{
    InitializeComponent();
    BindingContext = App.ContactsViewModel;  // Use shared instance
}
```

### 7. Route Registration (AppShell.xaml.cs)

```csharp
public AppShell()
{
    InitializeComponent();
    Routing.RegisterRoute("contactdetail", typeof(ContactDetailPage));
}
```

### 8. XAML TabBar Navigation

```xaml
<TabBar>
    <ShellContent Title="Add Contact"
                  ContentTemplate="{DataTemplate views:AddContactPage}"
                  Route="addcontactpage" />
    
    <ShellContent Title="Contacts"
                  ContentTemplate="{DataTemplate views:ContactsPage}"
                  Route="contactspage" />
</TabBar>
```

## Performance Tips

1. **ObservableCollection**: Use for dynamic lists that need to update UI automatically
2. **RelayCommand**: More efficient than traditional ICommand implementations
3. **Data Binding**: Prefer binding over code-behind manipulation for better MVVM patterns
4. **View Caching**: Shell automatically caches views for better performance

## Common Patterns

### Pattern 1: Form Input with Binding
```csharp
[ObservableProperty]
private string? email;
```

```xaml
<Entry Text="{Binding Email}" Keyboard="Email" />
```

### Pattern 2: List Selection with Navigation
```csharp
[RelayCommand]
public async Task SelectContact(Contact contact)
{
    SelectedContact = contact;
    await Shell.Current.GoToAsync("contactdetail");
}
```

```xaml
<CollectionView SelectionChangedCommand="{Binding SelectContactCommand}"
                SelectionChangedCommandParameter="{Binding SelectedItem, Source={RelativeSource Self}}" />
```

### Pattern 3: Command Validation
```csharp
[RelayCommand]
public async Task SaveContact()
{
    if (!ValidateInput())
        return;
    
    // Process save
}

private bool ValidateInput()
{
    return !string.IsNullOrWhiteSpace(Name);
}
```

## Project Dependencies

- **CommunityToolkit.Mvvm**: Provides MVVM attributes and utilities
- **Microsoft.Maui.Controls**: MAUI UI framework
- **.NET 8.0 / 9.0**: Target frameworks for various platforms

## Useful Links

- [.NET MAUI Documentation](https://learn.microsoft.com/en-us/dotnet/maui/)
- [MVVM Toolkit Documentation](https://learn.microsoft.com/en-us/windows/communitytoolkit/mvvm/)
- [Shell Navigation](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/)
- [Data Binding](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/)
