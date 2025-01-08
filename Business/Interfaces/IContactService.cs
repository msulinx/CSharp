
using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    event EventHandler ContactsUpdated;
    
    bool CreateContact(CreateContactForm form);
    
    bool AddContactToList(Contact contact);

    void SaveContactList();
    
    bool EditContact(Contact contact);
    
    bool DeleteContact (Contact contact);

    IEnumerable<Contact> GetAllContacts();
}