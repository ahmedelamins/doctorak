namespace Doctorak.Server.Models;
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    //first and last name for clarity 
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;

    // register with an email OR a phone number
    [EmailAddress]
    public string? Email { get; set; } = string.Empty;
    public string? Number { get; set; } = string.Empty;

    //trcack email/number confirmation
    public bool IsEmailConfirmed { get; set; } = false;
    public bool IsNumberConfirmed { get; set; } = false;

    //store code/token for verifications
    public string? EmailConfirmationToken { get; set; }
    public string? NumberConfirmationToken { get; set; }

    //password hash and salt
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
