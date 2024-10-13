namespace Doctorak.Server.Services.AdminServicel;

public class AdminService : IAdminService
{
    private readonly DataContext _context;

    public AdminService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<int>> AdminReginster(Admin admin, string password)
    {
        var response = new ServiceResponse<int>();

        try
        {
            if (await UserExists(admin.Username))
            {
                response.Success = false;
                response.Message = "Username taken";

                return response;
            }



            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public Task<ServiceResponse<string>> AdminLogin(string username, string password)
    {
        throw new NotImplementedException();
    }
    public Task<ServiceResponse<bool>> AdminDelete(int userId)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Admins.AnyAsync(a =>
            a.Username.ToLower().Equals(username.ToLower()));
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
}
