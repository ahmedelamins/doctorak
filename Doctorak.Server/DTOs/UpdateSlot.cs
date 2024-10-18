namespace Doctorak.Server.DTOs;

public class UpdateSlot
{
    public string Day { get; set; } = string.Empty;
    public TimeOnly Starts { get; set; }
    public TimeOnly Ends { get; set; }
}
