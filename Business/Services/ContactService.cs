
using System.Diagnostics;
using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private List<Contact> _contactList = [];
    
    public event EventHandler? ContactsUpdated;

    /* Strukturen av att dela upp funktionerna i egna metoder är genererat av Chat GPT 4.0.
    Detta för att följa S i SOLID och kunna moqtesta metoderna enskilt */

    public bool CreateContact(CreateContactForm form)

    {
        try
        {
            var contact = ContactFactory.Create(form);

            SetId(contact); // Genererar ett id för varje kontakt

            AddContactToList(contact); // Lägger till kontakt i listan

            SaveContactList(); // Sparar kontakten i listan
            
            ContactsUpdated?.Invoke(this, EventArgs.Empty); // Triggar uppdatering
            
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool AddContactToList(Contact contact)
    {
        try
        {
            _contactList.Add(contact);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }


    public void SaveContactList()
    {
        try
        {
            
           contactRepository.SaveContacts(_contactList);
           
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }


    public void SetId(Contact contact)
    {
        try
        {
            contact.Id = IdGenerator.GenerateId();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }


    public IEnumerable<Contact> GetAllContacts()
    {
        try
        {
            _contactList = contactRepository.GetContacts(); 
            return _contactList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
    
    
    public bool EditContact(Contact contact)
    {
        try
        {
            var contactPerson = _contactList.FirstOrDefault(c => c.Id == contact.Id);
        
            if (contactPerson == null)
            {
                return false;
            }
            
            contactPerson.FirstName = contact.FirstName;
            contactPerson.Address = contact.Address;
            
            SaveContactList();
            
            ContactsUpdated?.Invoke(this, EventArgs.Empty);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteContact(Contact contact)
    {
        try
        {
            
            var contactPerson = _contactList.FirstOrDefault(c => c.Id == contact.Id);
            
            if (contactPerson == null)
            {
                return false;
            }

            _contactList.Remove(contactPerson);
            
            SaveContactList();
            
            ContactsUpdated?.Invoke(this, EventArgs.Empty);
            
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    
    public void ContactListToTest (List<Contact> contacts) // Används för enhetstester
    {
        _contactList = contacts;
    }
}