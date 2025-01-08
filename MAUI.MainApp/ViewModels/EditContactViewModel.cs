
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = Business.Models.Contact;

namespace MAUI.MainApp.ViewModels;

public partial class EditContactViewModel : ObservableObject, IQueryAttributable
{
    private readonly IContactService _contactService;

    public EditContactViewModel(IContactService contactService)
    {
        _contactService = contactService;
    }

    [ObservableProperty] private Contact _contact = new();
    
    [RelayCommand]
    
    private async Task EditContact()
    {
        var result = _contactService.EditContact(Contact);
        if (result)
        {
            await Shell.Current.GoToAsync("//ContactListPage");
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Contact"))
        {
            Contact = (Contact)query["Contact"];
        }
    }
}