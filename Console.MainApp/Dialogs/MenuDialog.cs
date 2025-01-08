
using System.ComponentModel.DataAnnotations;
using Business.Interfaces;
using Business.Models;


namespace MainApp.Dialogs;

public class MenuDialog(IContactService contactService)
{
    public void Run()
    {
        while (true)
        {
            var option = MainMenu();
            
            if (!string.IsNullOrEmpty(option))
            {
                MenuOptionSelection(option);
            }
            else
            {
                Console.WriteLine("You must enter a valid option.");
                Console.ReadKey();
            }

            return;
        }
    }
    
    private string MainMenu()
    {

        Console.Clear();
        Console.WriteLine("---------- WELCOME TO YOUR CONTACTLIST ----------");
        Console.WriteLine("Choose your option: ");
        Console.WriteLine($"{"1. ", -3} ADD A NEW CONTACT");
        Console.WriteLine($"{"2. ", -3} VIEW ALL CONTACTS");
        Console.WriteLine($"{"3. ", -3} QUIT APPLICATION");
            
        Console.Write("Selected option: ");
        var option = Console.ReadLine()!;
            
        return option;
    
    }
    
    private void MenuOptionSelection(string option)
    {
        switch (option.ToLower())
        {
            case "1":
                AddContact();
                break;
            
            case "2":
                ViewAllContacts();
                break;
            
            
            case "3":
                QuitApplication();
                break;
            
            default:
                Console.WriteLine("Please enter a valid option.");
                break;
        }
    }

    private void AddContact()
    {
        Console.Clear();
        Console.WriteLine("---------- CREATE CONTACT ----------\n");

        var form = new CreateContactForm();
        
        form.FirstName = PromptAndValidate("Enter first name: ", nameof(form.FirstName));
        
        form.LastName = PromptAndValidate("Enter last name: ", nameof(form.LastName));
        
        form.Email = PromptAndValidate("Enter email: ", nameof(form.Email));
        
        form.PhoneNumber = PromptAndValidate("Enter phone number: ", nameof(form.PhoneNumber));

        form.Address = PromptAndValidate("Enter street name: ", nameof(form.Address));
        
        form.ZipCode = PromptAndValidate("Enter zipcode: ", nameof(form.ZipCode));
        
        form.City = PromptAndValidate("Enter city: ", nameof(form.City));
        
        bool result = contactService.CreateContact(form);
        
        if (result)
            OutputDialog("Contact has been created successfully!");
        else
            OutputDialog("Failed to create contact. Please try again.");
        
        Console.ReadKey();

    }

    private void ViewAllContacts()
    {

        var contacts = contactService.GetAllContacts();
        
        Console.Clear();

        foreach (var contact in contacts)
        {
            Console.WriteLine($"{"ID: ",-15}{contact.Id}");
            Console.WriteLine($"{"Name: ",-15}{contact.FirstName} {contact.LastName}");
            Console.WriteLine($"{"Email: ",-15}{contact.Email}");
            Console.WriteLine($"{"PhoneNumber: ",-15}{contact.PhoneNumber}");
            Console.WriteLine($"{"Address: ",-15}{contact.Address}, {contact.ZipCode}, {contact.City}");
            Console.WriteLine("");
        }

        Console.ReadKey();

    }

    private void QuitApplication()
    {
        Console.Clear();
        Console.WriteLine("---------- QUIT APPLICATION ----------\n");
        Console.WriteLine("Are you sure you want to exit? (y/n)");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
    }
    

    private void OutputDialog(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.ReadKey();
    }

    private string PromptAndValidate(string prompt, string propertyName)
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write(prompt);
            var input = Console.ReadLine() ?? string.Empty;
            
            var results = new List<ValidationResult>();
            var context = new ValidationContext(new CreateContactForm()) { MemberName = propertyName };
            
            if (Validator.TryValidateProperty(input, context, results))
                return input;
            
            Console.WriteLine($"{results[0].ErrorMessage}. Please try again.");
        }
    }
}