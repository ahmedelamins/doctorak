namespace Doctorak.Server.DTOs;
public class UserRegister
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required, Compare("Password", ErrorMessage = "passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
