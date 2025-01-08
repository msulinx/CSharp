
using Business.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessTests.Services;

public class FileServiceTests
{

    [Fact]
    public void SaveContentToFile_ShouldSaveContentToFile()
    {
        // arrange
        var content = "Test Content";
        var directoryPath = Path.Combine(Path.GetTempPath(), "Test");
        var fileName = $"{Guid.NewGuid().ToString()}.json";
        
        var fileService = new FileService(directoryPath, fileName);

        try
        {
            // act
            var result = fileService.SaveContentToFile(content);
            
            // assert
            Assert.True(result);
        }
        finally
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnContent_WhenFileExists()
    {
        // arrange
        var directoryPath = Path.Combine(Path.GetTempPath(), "Test");
        var fileName = "test";
        var filePath = Path.Combine(directoryPath, fileName);
        var content = "Test Content";

        File.WriteAllText(filePath, content);

        var fileService = new FileService(directoryPath, fileName);

        try
        {
            // act
            var result = fileService.GetContentFromFile();

            // assert
            Assert.Equal(content, result);
        }
        finally
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }
    
}