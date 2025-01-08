using Business.Helpers;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessTests.Helpers;

public class IdGeneratorTests
{
    [Fact]
    public void GenerateId_Should_ReturnGuidAsAString()
    {
        // act
        var result = IdGenerator.GenerateId();
        
        //assert
        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.True(Guid.TryParse(result, out _));
    }
}