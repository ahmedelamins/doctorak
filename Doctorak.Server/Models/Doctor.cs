namespace Doctorak.Server.Models;

public class Doctor : User
{
    [Required]
    public string Gender { get; set; } = string.Empty;
    [Required]
    public string Specialties { get; set; } = string.Empty;
    [Required]
    public string PracticeName { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public List<string> Qualifications { get; set; } = new List<string>();
    [Required]
    public string About { get; set; } = string.Empty;
    public List<AvailabilitySlot> AvailabilitySlots { get; set; } = new List<AvailabilitySlot>();
}
