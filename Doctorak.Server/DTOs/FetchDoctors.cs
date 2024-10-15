namespace Doctorak.Server.DTOs;

public class FetchDoctors
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Specialties { get; set; } = string.Empty;
    public string PracticeName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<string> Qualifications { get; set; } = new List<string>();
    public string About { get; set; } = string.Empty;
}
