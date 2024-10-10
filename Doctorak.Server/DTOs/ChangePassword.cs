namespace Doctorak.Server.DTOs;
public class ChangePassword
{
    public string Password { get; set; } = string.Empty;
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
