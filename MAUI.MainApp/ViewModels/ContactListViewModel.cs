
using System.Collections.ObjectModel;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = Business.Models.Contact;

namespace MAUI.MainApp.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    public ContactListViewModel(IContactService contactService)
    {
        _contactService = contactService;

        // Kapslar in LoadContacts i trigger eventet
        _contactService.ContactsUpdated += (sender, e) => 
        
        {
            LoadContacts();
        };
        
        LoadContacts(); // Kontaktlista visas på första sidan
    }

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = new ObservableCollection<Contact>();
    
    [RelayCommand]
    private void LoadContacts() // Hämta kontaktlista
    {
        var contacts = _contactService.GetAllContacts();
        ContactList = new ObservableCollection<Contact>(contacts);
    }

    [RelayCommand]
    public async Task NavigateToEdit(Contact contact)
    {
        var parameters = new ShellNavigationQueryParameters()
        {
            { "Contact", contact }
        };
        await Shell.Current.GoToAsync("//EditContactPage", parameters);
    }
    [RelayCommand]
    public void DeleteContact(Contact contact)
    {
       var result = _contactService.DeleteContact(contact);

        if (result)
        {
            ContactList = new ObservableCollection<Contact>(_contactService.GetAllContacts());
        }
    }
}