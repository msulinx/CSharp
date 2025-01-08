using Business.Factories;
using Business.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessTests.Factories;

public class ContactFactoryTests
{

    [Fact]
    public void Create_ShouldCreateContactForm()
    {
        
        // act
        CreateContactForm result = ContactFactory.Create();

        // assert
        Assert.IsType<CreateContactForm>(result);
    }
        
    
    // Kontrollerar om CreateContactForm returnerar Contact //
    
    [Fact]
    public void CreateContact_ShouldConvertForm_ToContact()
    {
        // arrange
        var form = new CreateContactForm { FirstName = "Test", Email = "test@test.com" };
        
        // act
        var result = ContactFactory.Create(form);
        
        //assert
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
    }
}