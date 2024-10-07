using Doctorak.Server.Services.EmailService;

namespace Doctorak.Server.Services.AuthService;
public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public AuthService(DataContext context, IConfiguration configuration, IEmailService emailService)
    {
        _context = context;
        _configuration = configuration;
        _emailService = emailService;
    }
    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        var response = new ServiceResponse<int>();

        try
        {
            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "this Email is already registerd!";

                return response;
            }
            else if (!ValidPassword(password))
            {
                response.Success = false;
                response.Message = "Invalid password.";

                return response;
            }
            else
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                //generate verification code 
                var confirmationToken = Guid.NewGuid().ToString();
                user.EmailConfirmationToken = confirmationToken;
                user.IsEmailConfirmed = false;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                //send confirmation email
                var confirmationUrl = $"{_configuration["AppUrl"]}/confirm-email?token={confirmationToken}&email={user.Email}";
                var emailBody = $"Please confirm your email by clicking <a href='{confirmationUrl}'>here</a>.";

                await _emailService.SendEmail(user.Email, user.FirstName, "Confirm Your Email", emailBody);

                response.Data = user.Id;
                response.Message = "Welcome!";
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
    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(u =>
            u.Email.ToLower().Equals(email.ToLower())
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

    //password hash and salt
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

}
