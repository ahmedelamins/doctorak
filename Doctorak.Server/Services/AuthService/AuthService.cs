namespace Doctorak.Server.Services.AuthService;
public class AuthService : IAuthService
{
    private readonly DataContext _context;

    public AuthService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        var response = new ServiceResponse<int>();

        try
        {

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    //check if a user exists
    public async Task<bool> UserExists(string email, string number)
    {
        return await _context.Users.AnyAsync(u =>
            (u.Email != null && u.Email.ToLower().Equals(email.ToLower())) ||
            (u.Number != null && u.Number.Equals(number))
        );
    }

}
