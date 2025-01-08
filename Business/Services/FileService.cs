using System.Diagnostics;
using Business.Interfaces;

namespace Business.Services;

public sealed class FileService : IFileService
{

    private readonly string _directoryPath;
    private readonly string _filePath;


    public FileService(string directoryPath, string fileName)
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
    }
    public bool SaveContentToFile(string content)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            File.WriteAllText(_filePath, content);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public string GetContentFromFile()
    {
        try
        {
            return File.ReadAllText(_filePath);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}