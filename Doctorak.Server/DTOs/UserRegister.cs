namespace Doctorak.Server.DTOs;
public class UserRegister
{
    public string? Email { get; set; }
    public string? Number { get; set; }
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required, Compare("Password", ErrorMessage = "passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    //validation to ensure either phone number or email is provided
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Number))
        {
            yield return new ValidationResult("Either Email or Phone number must be provided.");
        }
        // Check if the phone number is valid if provided
        if (!string.IsNullOrEmpty(Number) && !System.Text.RegularExpressions.Regex.IsMatch(Number, @"^\d+$"))
        {
            yield return new ValidationResult("Invalid phone number format. Only digits are allowed.");
        }
    }
}
