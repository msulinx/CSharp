using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUI.MainApp.ViewModels;

public partial class AddContactViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    public AddContactViewModel(IContactService contactService)
    {
        _contactService = contactService;
        ContactForm = new CreateContactForm();
    }

    [ObservableProperty]
    private CreateContactForm _contactForm;

    [RelayCommand]
    private async Task CreateContact()
    {
        if (_contactService.CreateContact(ContactForm))
        {
            ContactForm = new CreateContactForm();
            await Shell.Current.GoToAsync("//ContactListPage");
        }
    }
}