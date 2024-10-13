namespace Doctorak.Server.Services.AdminService;

public interface IAdminService
{
    Task<ServiceResponse<int>> AdminReginster(User user, string password);
    Task<ServiceResponse<string>> AdminLogin(string name, string password);
    Task<ServiceResponse<bool>> AdminDelete(int userId);
}
