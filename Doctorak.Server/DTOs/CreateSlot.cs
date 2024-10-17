namespace Doctorak.Server.DTOs;

public class CreateSlot
{
    public int DoctorId { get; set; }
    [Required]
    public string Day { get; set; } = string.Empty;
    [Required]
    public TimeOnly Starts { get; set; }
    [Required]
    public TimeOnly Ends { get; set; }
}
