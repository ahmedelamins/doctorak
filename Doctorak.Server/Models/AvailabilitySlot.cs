namespace Doctorak.Server.Models;

public class AvailabilitySlot
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int DoctorId { get; set; }
    [Required]
    public string Day { get; set; } = string.Empty;
    [Required]
    public TimeOnly Starts { get; set; }
    [Required]
    public TimeOnly Ends { get; set; }
    public List<BreakSlot> Breaks { get; set; } = new List<BreakSlot>();
}