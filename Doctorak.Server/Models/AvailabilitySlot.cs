namespace Doctorak.Server.Models;

public class AvailabilitySlot
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan Starts { get; set; }
    public TimeSpan Ends { get; set; }
}
