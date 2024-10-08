namespace Doctorak.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly DataContext _context;

    public AuthService(DataContext context)
    {
        _context = context;
    }
    public Task<ServiceResponse<int>> Register(User user, string password)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(
            u => u.Email.ToLower().Equals(email.ToLower())
        );
    }
}
