using Doctorak.Server.Services.EmailService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

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
                response.Message = "Email is already used";

                return response;
            }

            if (!ValidPassword(password))
            {
                response.Success = false;
                response.Message = "Invalid password";
                return response;
            }


            user.VerificationCode = GenerateRandomCode();

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            string emailBody = "<h4>Your verification code is:</h4>" +
                               $"<h1>{user.VerificationCode}</1>";


            await _emailService.SendEmail(user.Email, "Email Verification Code", emailBody);

            response.Data = user.Id;
            response.Message = "User created, please confirm email address";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }
    public async Task<ServiceResponse<string>> VerifyEmail(string email, string code)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found!";

                return response;
            }

            if (user.VerificationCode != code)
            {
                response.Success = false;
                response.Message = "Invalid code";

                return response;
            }

            user.Verified = true;
            user.VerificationCode = null;

            await _context.SaveChangesAsync();

            string emailBody = "<h1>Welcome to Doctorak!</h1>" +
                                  $"<h2>Our team is very happy to have you.</h2>";


            await _emailService.SendEmail(user.Email, "Email Verified!", emailBody);

            response.Message = "Email verified successfully";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }

    }

    public async Task<ServiceResponse<string>> ForgotPassword(string email)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";

                return response;
            }

            user.VerificationCode = GenerateRandomCode();

            await _context.SaveChangesAsync();

            string emailBody = "<h4>Your password reset code is:</h4>" +
                                   $"<h1>{user.VerificationCode}</1>";

            await _emailService.SendEmail(user.Email, "Reset Your Password", emailBody);

            response.Message = "Password reset code has been sent to your email";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<string>> VerifyPasswordReset(string email, string code)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found!";

                return response;
            }

            if (user.VerificationCode != code)
            {
                response.Success = false;
                response.Message = "Invalid code";

                return response;
            }

            user.PasswordResetCode = null;

            await _context.SaveChangesAsync();

            response.Message = "Password reset verified";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower()
            .Equals(email.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User does not exist";

                return response;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";

                return response;
            }

            response.Data = CreateToken(user);
            response.Message = "Welcome back!";

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }

    public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found!";

                return response;
            }

            if (VerifyPasswordHash(newPassword, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Please enter a new password";

                return response;
            }

            if (!ValidPassword(newPassword))
            {
                response.Success = false;
                response.Message = "Invalid Password";

                return response;
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "Password has been changed.";

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }

    //check user exists
    private async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(
            u => u.Email.ToLower().Equals(email.ToLower())
        );
    }

    //validate password
    private bool ValidPassword(string password)
    {
        return password.Length >= 5;
    }

    //hash password   
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    //verify passwords
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computeHash =
            hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computeHash.SequenceEqual(passwordHash);
        }
    }

    //generating random code
    private string GenerateRandomCode()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    //create token
    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));


        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken
            (
              claims: claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

}
