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
}
