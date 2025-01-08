using System.Diagnostics;
using Business.Models;

namespace Business.Factories;

public static class ContactFactory
{
    public static CreateContactForm Create()
    {
        return new CreateContactForm();
    }

    public static Contact Create(CreateContactForm form) // Gör om CCF till Contact
    {
        try
        {
            return new Contact
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
                Address = form.Address,
                ZipCode = form.ZipCode,
                City = form.City
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}