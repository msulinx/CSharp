using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class CreateContactForm
{
    [Required (ErrorMessage = "First name is required")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters long.")]
    public string FirstName { get; set; } = null!;
    
    [Required (ErrorMessage = "Last name is required")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long.")]
    public string LastName { get; set; } = null!;
    
    [Required (ErrorMessage = "Email is required")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "Email must be a valid form, ex. a@b.cd.")]
    public string Email { get; set; } = null!;
    
    [Required (ErrorMessage = "Phone Number is required")]
    [MinLength(7, ErrorMessage = "Phone number must be at least 7 digits long.")]
    public string PhoneNumber { get; set; } = null!;
    
    [Required (ErrorMessage = "Address is required")]
    [MinLength(2, ErrorMessage = "Address must be at least 2 characters long.")]
    public string Address { get; set; } = null!;
    
    [Required (ErrorMessage = "Zip Code is required")]
    [MinLength(4, ErrorMessage = "Zip Code must be at least 4 characters long.")]
    public string ZipCode { get; set; } = null!;
    
    [Required (ErrorMessage = "City is required")]
    [MinLength(2, ErrorMessage = "City must be at least 2 characters long.")]
    public string City { get; set; } = null!;
}