namespace Doctorak.Server.Services.AdminService;

public interface IAdminService
{
    Task<ServiceResponse<int>> AdminRegister(Admin user, string password);
    Task<ServiceResponse<string>> AdminLogin(string username, string password);
    Task<ServiceResponse<bool>> DeleteUser(string email);
}
