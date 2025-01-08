
using Business.Models;

namespace Business.Interfaces;

public interface IContactRepository
{
    
    bool SaveContacts(List<Contact> contacts);
    
    List<Contact> GetContacts();
}