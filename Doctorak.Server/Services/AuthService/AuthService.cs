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
            if (await UserExists(user.Email, user.Number))
            {
                response.Success = false;
                response.Message = "Email or Number is already registerd.";

                return response;
            }
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
            (u.Email != null && u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) ||
            (u.Number != null && u.Number.Equals(number))
        );
    }

    //validate password
    private bool ValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 5)
        {
            return false;
        }

        return true;
    }

}
