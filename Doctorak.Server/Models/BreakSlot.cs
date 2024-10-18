namespace Doctorak.Server.Models;

public class BreakSlot
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int AvailabilitySlotId { get; set; }
    public TimeOnly BreakStarts { get; set; }

    public TimeOnly BreakEnds { get; set; }
}
