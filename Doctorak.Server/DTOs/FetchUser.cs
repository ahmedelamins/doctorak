namespace Doctorak.Server.DTOs;

public class FetchUser
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool Verified { get; set; }
    public DateTime JoinAt { get; set; }
}
