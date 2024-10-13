namespace Doctorak.Server.Services.AdminServicel;

public class AdminService : IAdminService
{
    private readonly DataContext _context;

    public AdminService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<int>> AdminReginster(User username, string password)
    {
        var response = new ServiceResponse<int>();

        try
        {
            if (await )
                return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
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
