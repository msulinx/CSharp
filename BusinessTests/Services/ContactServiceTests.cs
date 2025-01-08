
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessTests.Services;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly IContactService _contactService;
    
    public ContactServiceTests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _contactService = new ContactService(_contactRepositoryMock.Object);
    }

    [Fact]

    public void CreateContact_ShouldAddContactToList_AndSaveToFile()
    {
        // arrange
        var form = new CreateContactForm { FirstName = "Test", Email = "test@test.com" };

        _contactRepositoryMock
            .Setup(cr => cr.SaveContacts(It.IsAny<List<Contact>>()))
            .Returns(true);

        // act
        var result = _contactService.CreateContact(form);

        // assert
        Assert.True(result);
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<Contact>>()), Times.Once);
    }


    [Fact]
    public void CreateContact_ShouldGenerateANewId_ToEachContact() // Kontrollerar generering av ID för varje kontakt
    {
        // arrange
        ContactService contactService = new ContactService(_contactRepositoryMock.Object);
        var contact = new Contact();

        // act
        contactService.SetId(contact);

        // assert
        Assert.False(string.IsNullOrEmpty(contact.Id));
    }


    [Fact]
    public void AddContactToList_ShouldReturnTrue_WhenAdded()
    {
        // arrange
        ContactService contactService = new ContactService(_contactRepositoryMock.Object);
        var contact = new Contact
            { Id = "1", FirstName = "Test name", Address= "Test street name" };

        // act 
        bool result = contactService.AddContactToList(contact);

        // assert
        Assert.True(result);
    }

    [Fact]
    public void SaveContactList_ShouldCallRepositoryToSave_WhenContactIsAdded()
    {
        // arrange
        var contactService = new ContactService(_contactRepositoryMock.Object);
        _contactRepositoryMock
            .Setup(cr => cr.SaveContacts(It.IsAny<List<Contact>>()))
            .Returns(true);

        // act
        contactService.SaveContactList();

        // assert
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<Contact>>()), Times.Once);
    }


    [Fact]
    public void GetAllContacts_ReturnListOfContactsFromRepository_WhenCalled() // Kontrollerar att listan hämtas
    {
        // arrange
        var contacts = new List<Contact>()
        {
            new() { FirstName = "Test Contact 1", Address = "Test Address 1" },
            new() { FirstName = "Test Contact 1", Address = "Test Address 2" },
        };

        _contactRepositoryMock.Setup(cs => cs.GetContacts()).Returns(contacts);


        // act
        var result = _contactService.GetAllContacts().ToList();

        // assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Test Contact 1", result.First().FirstName);
        _contactRepositoryMock.Verify(cs => cs.GetContacts(), Times.Once);
    }

    [Fact]
    public void EditContact_ShouldFindContact_AndSaveUpdatedContactToList()
    {
        // arrange
        var contactList = new List<Contact>
        {
            new () { Id = "1", FirstName = "Test Contact 1", Address = "Test Address 1" },
            new () { Id = "2", FirstName = "Test Contact 2", Address = "Test Address 2" }
        };
    
        _contactRepositoryMock.Setup(cs => cs.GetContacts()).Returns(contactList);

        /* Denna del är genererad av Chat GPT 4.0. Koden är till för att använda en mockad lista
        i testets ContactService så att rätt lista uppdateras då listan
        i originalklassen är privat */
        
        var contactService = new ContactService(_contactRepositoryMock.Object);
        contactService.ContactListToTest(contactList);

        var contactToEdit = new Contact { Id = "1", FirstName = "Test Contact 1", Address = "New Test Address" };

        // act
        var result = contactService.EditContact(contactToEdit);

        // assert
        Assert.True(result);
        
        /* Denna kod är genererad av Chat GPT 4.0. Koden används för att kontrollera
         om den sparade listan är uppdaterad med den redigerade kontakten */
        
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.Is<List<Contact>>(list =>
            list.Count == 2 && // 2 kontakter ska finnas i listan
            list[0].Id == "1" && // Kontakten med ID 1
            list[0].Address == "New Test Address" && // Korrekt uppdaterad adress
            list[1].Id == "2" // Kontakten med ID 2
        )), Times.Once);
    }

    [Fact]
    public void DeleteContact_ShouldRemoveContact_AndSaveUpdatedList()
    {
        // arrange
        var contactList = new List<Contact>
        {
            new() { Id = "1", FirstName = "Test Contact 1", Address = "Test Street 1" },
            new() { Id = "2", FirstName = "Test Contact 2", Address = "Test Street 2" }
        };
        
        /* Denna del är genererad av Chat GPT 4.0. Koden är till för att använda en mockad lista
         i testets ContactService så att rätt lista uppdateras då listan
         i originalklassen är privat */

        var contactService = new ContactService(_contactRepositoryMock.Object);
        contactService.ContactListToTest(contactList);
        
        //*

        var contactToRemove = new Contact { Id = "1", FirstName = "Test Contact 1", Address = "Test Street 1" };

        // act
        var result = contactService.DeleteContact(contactToRemove);

        // assert
        Assert.True(result);
        
        /* Denna kod är genererad av Chat GPT 4.0 som kontrollerar om den sparade och
         uppdaterade listan innehåller 1 kontakt som har ID 2*/
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.Is<List<Contact>>(list =>
            list.Count == 1 && list[0].Id == "2"
        )), Times.Once);
    }
    
}