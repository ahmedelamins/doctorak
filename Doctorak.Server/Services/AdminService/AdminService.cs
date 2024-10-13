namespace Doctorak.Server.Services.AdminServicel;

public class AdminService : IAdminService
{
    private readonly DataContext _context;

    public AdminService(DataContext context)
    {
        _context = context;
    }

    public Task<ServiceResponse<int>> AdminReginster(User user, string password)
    {
        throw new NotImplementedException();
    }
    public Task<ServiceResponse<string>> AdminLogin(string name, string password)
    {
        throw new NotImplementedException();
    }
    public Task<ServiceResponse<bool>> AdminDelete(int userId)
    {
        throw new NotImplementedException();
    }

}
