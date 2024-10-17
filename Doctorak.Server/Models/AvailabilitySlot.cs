namespace Doctorak.Server.Models;

public class AvailabilitySlot
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
