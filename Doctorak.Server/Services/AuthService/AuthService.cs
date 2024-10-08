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
            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "Email is already in use";

                return response;
            }
            else if (!ValidPassword(password))
            {
                response.Success = false;
                response.Message = "Invalid password";

            }

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    //validate password
    private bool ValidPassword(string password)
    {
        return password.Length > 5;
    }

    //check user exists
    private async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(
            u => u.Email.ToLower().Equals(email.ToLower())
        );
    }
}
