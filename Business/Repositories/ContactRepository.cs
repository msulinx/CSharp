using System.Text.Json;
using Business.Interfaces;
using Business.Models;

namespace Business.Repositories;

public class ContactRepository : IContactRepository

{
    private readonly IFileService _fileService;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new ()
        { WriteIndented = true };

    public ContactRepository(IFileService fileService)
    {
        _fileService = fileService;
    }


    public bool SaveContacts(List<Contact> list)
    {
        try
        {
            
            var json = JsonSerializer.Serialize(list, _jsonSerializerOptions);
            _fileService.SaveContentToFile(json);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public List<Contact> GetContacts()
    {
        try
        {
            var json = _fileService.GetContentFromFile();
            var list = JsonSerializer.Deserialize<List<Contact>>(json);
            return list ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
}