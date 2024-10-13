using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Doctorak.Server.Services.AdminServicel;

public class AdminService : IAdminService
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;

    public AdminService(DataContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<ServiceResponse<int>> AdminRegister(Admin admin, string password)
    {
        var response = new ServiceResponse<int>();

        try
        {
            if (await AdminExists(admin.Username))
            {
                response.Success = false;
                response.Message = "Username taken";

                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            response.Data = admin.Id;
            response.Message = $"Welcome, admin {admin.Username}";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public async Task<ServiceResponse<string>> AdminLogin(string username, string password)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username.ToLower().Equals(username.ToLower()));

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }

            if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";

                return response;
            }

            var token = CreateToken(admin);

            response.Data = token;
            response.Message = $"Welcome, {admin.Username}";

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }
    public async Task<ServiceResponse<bool>> DeleteUser(int userId)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Not found";
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "User delete.";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public async Task<ServiceResponse<List<FetchUser>>> GetUsers()
    {
        var response = new ServiceResponse<List<FetchUser>>();

        try
        {
            var users = await _context.Users.Select(u =>
                new FetchUser
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Verified = u.Verified,
                    JoinAt = u.CreatedAt
                }
            ).ToListAsync();

            response.Data = users;

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    private async Task<bool> AdminExists(string username)
    {
        return await _context.Admins.AnyAsync(a =>
            a.Username.ToLower().Equals(username.ToLower()));
    }
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computeHash =
            hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computeHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(Admin admin)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            new Claim(ClaimTypes.Name, admin.Username)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken
            (
               claims: claims,
               expires: DateTime.Now.AddHours(1),
               signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
