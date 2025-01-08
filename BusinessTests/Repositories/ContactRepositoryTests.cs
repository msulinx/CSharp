using System.Text.Json;
using Business.Interfaces;
using Business.Models;
using Business.Repositories;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessTests.Repositories;

public class ContactRepositoryTests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IContactRepository _contactRepository;

    public ContactRepositoryTests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactRepository = new ContactRepository(_fileServiceMock.Object);
    }

    [Fact]
    public void SaveContacts_ShouldReturnTrue_AndSaveSerializedListToFile()
    {
        // arrange
        var list = new List<Contact>
        {
            new () { Id = "1", FirstName = "First name test", LastName = "Last name test" }
        };

        var option = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        var json = JsonSerializer.Serialize(list, option);
        
        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);
        
        // act
        var result = _contactRepository.SaveContacts(list);
        
        // assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.Is<string>(s => s == json)), Times.Once);
    }


    [Fact]

    public void GetContacts_ShouldReturnDeserializedList()
    {
        // arrange
        var list = new List<Contact>()
        {
            new() { FirstName = "Test Contact 1", Address = "Test Street 1" },
            new () { FirstName = "Test Contact 1", Address = "Test Street 2" }
        };
        
        var json =JsonSerializer.Serialize(list);
        
        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(json);

        // act
        var result = _contactRepository.GetContacts();

        // assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Test Contact 1", result.First().FirstName);
    }
    
    
}