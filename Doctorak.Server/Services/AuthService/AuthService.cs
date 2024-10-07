﻿namespace Doctorak.Server.Services.AuthService;
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

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

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
