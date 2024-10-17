namespace Doctorak.Server.Models;

public class Appointment
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string? CancellationReason { get; set; }
}
