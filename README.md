# Contact List Application - MVVM Implementation

## Project Overview

This is a .NET MAUI mobile application built following the **MVVM (Model-View-ViewModel)** architecture pattern. The application allows users to manage contacts efficiently with a clean, intuitive user interface.

## Architecture & Implementation

### 1. **MVVM Architecture**

The application is structured with clear separation of concerns:

- **Models** (`Models/Contact.cs`): Represent the business entities
- **ViewModels** (`ViewModels/ContactsViewModel.cs`): Handle business logic and state management
- **Views** (`Views/*.xaml`): Display UI and bind to ViewModels

### 2. **Technology Stack**

- **Framework**: .NET MAUI (Multi-platform App UI)
- **State Management**: CommunityToolkit.MVVM
- **UI Framework**: XAML with Data Binding
- **Platforms**: iOS, Android, macOS Catalyst, Windows

### 3. **Key Components**

#### Model: Contact.cs
```csharp
public class Contact
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
}
```

#### ViewModel: ContactsViewModel.cs
- Uses `[ObservableProperty]` from CommunityToolkit.MVVM for automatic property notifications
- Manages an `ObservableCollection<Contact>` for dynamic UI updates
- Implements `[RelayCommand]` for button actions with dependency injection
- Handles navigation between pages

**Key Features:**
- `SaveContactCommand`: Validates and adds new contacts
- `SelectContactCommand`: Navigates to contact details
- `GoToAddContactCommand`: Navigates to add contact page
- `GoToContactsListCommand`: Navigates to contacts list
- `GoBackCommand`: Navigates back in the navigation stack

## Application Pages

### 1. **Add Contact Page** (AddContactPage.xaml)

**Location**: Tab 1 in TabBar

**Features:**
- Form with input fields:
  - Name (required)
  - Email
  - Phone Number (with phone keyboard)
  - Description (multi-line text area)
- Save Contact button: Validates name field and adds contact to list
- View Contacts button: Navigates to contacts list
- Form validation ensures at least a name is provided
- Automatic form clearing after successful save

### 2. **Contacts Page** (ContactsPage.xaml)

**Location**: Tab 2 in TabBar

**Features:**
- Displays all contacts in a `CollectionView`
- Shows contact name and email for each item
- Each contact is displayed in a styled Frame with shadow
- Tap to select a contact and view details
- "Add New Contact" button for quick access
- Responsive layout that fills available space
- Dynamic UI updates when contacts are added

### 3. **Contact Details Page** (ContactDetailPage.xaml)

**Features:**
- Displays complete contact information:
  - Name
  - Email
  - Phone Number
  - Description
- Information displayed in organized sections with separators
- "Back to Contacts" button: Returns to contacts list
- "Add Another Contact" button: Navigates to add contact form
- Uses the shared ViewModel to access selected contact data

## Navigation Flow

```
┌─────────────────────────┐
│   Add Contact Page      │
│   (Tab 1)               │
│ • Enter Details         │
│ • Click Save ──────┐    │
│ • View Contacts ──┐│    │
└─────────────────────────┘
                    ││
                    │└───────┬────────────────┐
                    │        │                │
                    ↓        ↓                ↓
            ┌────────────────────────────────────┐
            │    Contacts Page (Tab 2)           │
            │ • View List of Contacts            │
            │ • Tap to View Details ──────────┐  │
            │ • Add New Contact ────────────┐ │  │
            └────────────────────────────────────┘
                            │        ↑      │
                    ┌───────┘        │      └────────┐
                    │                │               │
                    ↓                ↑               │
            ┌──────────────────┐  Back to List  │
            │ Contact Details  │◄───────────────┘
            │ • Full Info      │
            │ • Back Button    │
            │ • Add Another    │
            └──────────────────┘
```

## Data Binding

The application uses XAML data binding with the following patterns:

1. **Two-Way Binding** for input controls:
   ```xaml
   <Entry Text="{Binding Name}" />
   ```

2. **Command Binding** for buttons:
   ```xaml
   <Button Command="{Binding SaveContactCommand}" />
   ```

3. **Collection Binding** for displaying lists:
   ```xaml
   <CollectionView ItemsSource="{Binding Contacts}" />
   ```

4. **Property Binding** for detail displays:
   ```xaml
   <Label Text="{Binding SelectedContact.Name}" />
   ```

## State Management

- **Shared ViewModel Instance**: A single `ContactsViewModel` instance is maintained throughout the app lifecycle via `App.ContactsViewModel`
- **ObservableCollection**: All contacts are stored in an `ObservableCollection<Contact>` which automatically notifies the UI of changes
- **Observable Properties**: All data properties use `[ObservableProperty]` for automatic change notification

## Key Implementation Details

### 1. Dependency Registration (MauiProgram.cs)
```csharp
builder
    .UseMauiApp<App>()
    .ConfigureFonts(fonts =>
    {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    });
```

### 2. Routing Configuration (AppShell.xaml.cs)
```csharp
public AppShell()
{
    InitializeComponent();
    Routing.RegisterRoute("contactdetail", typeof(ContactDetailPage));
}
```

### 3. Shared ViewModel (App.xaml.cs)
```csharp
public static ContactsViewModel ContactsViewModel { get; set; } = null!;

public App()
{
    InitializeComponent();
    ContactsViewModel = new ContactsViewModel();
}
```

## UI/UX Design

- **Color Scheme**: 
  - Primary: #512BD4 (Purple)
  - Secondary: #999999 (Gray)
  - Text: #333333 / #666666

- **Components**:
  - Frame elements for card-like presentation
  - Rounded corners (CornerRadius) for modern look
  - Shadows for depth
  - Clear separation between sections
  - Large, readable fonts for accessibility

- **Responsive Layout**:
  - VerticalStackLayout for page structure
  - HorizontalOptions/VerticalOptions for flexible layouts
  - FillAndExpand for content that should use available space

## Running the Application

### Build Commands:
```bash
# iOS Simulator
dotnet build -f net8.0-ios

# Android
dotnet build -f net9.0-android

# macOS Catalyst
dotnet build -f net8.0-maccatalyst

# Windows
dotnet build -f net9.0-windows10.0.19041.0
```

### Run Commands:
```bash
# iOS Simulator
dotnet run -f net8.0-ios -c Debug

# Android
dotnet run -f net9.0-android -c Debug
```

## File Structure

```
gaddammd_ContactListApplication/
├── App.xaml
├── App.xaml.cs              (Main app with shared ViewModel)
├── AppShell.xaml            (Navigation shell with TabBar)
├── AppShell.xaml.cs         (Route registration)
├── GlobalXmlns.cs
├── MainPage.xaml
├── MainPage.xaml.cs
├── MauiProgram.cs           (App configuration)
├── Models/
│   └── Contact.cs           (Contact model)
├── ViewModels/
│   └── ContactsViewModel.cs (Shared ViewModel with MVVM Toolkit)
├── Views/
│   ├── AddContactPage.xaml
│   ├── AddContactPage.xaml.cs
│   ├── ContactsPage.xaml
│   ├── ContactsPage.xaml.cs
│   ├── ContactDetailPage.xaml
│   └── ContactDetailPage.xaml.cs
├── Resources/
│   ├── AppIcon/
│   ├── Fonts/
│   ├── Images/
│   ├── Splash/
│   └── Styles/
└── Platforms/
    ├── Android/
    ├── iOS/
    ├── MacCatalyst/
    ├── Tizen/
    └── Windows/
```

## Features Implemented

✅ MVVM Architecture with CommunityToolkit.MVVM
✅ ObservableCollection for dynamic contact management
✅ Three dedicated pages for different operations
✅ Tab-based navigation with stack navigation support
✅ Input validation (Name field required)
✅ Two-way data binding
✅ RelayCommand for button actions
✅ Cross-platform support (iOS, Android, macOS, Windows)
✅ Responsive UI design
✅ Contact details view
✅ Form clearing after save
✅ Intuitive user flow

## Notes

- The application persists the contacts list during the current app session
- For persistent storage across sessions, consider adding JSON serialization or database integration
- The shared ViewModel instance ensures data consistency across all pages
- Navigation uses shell-based routing for smooth transitions
