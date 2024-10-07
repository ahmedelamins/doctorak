namespace Doctorak.Server.Services.AuthService;
public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
}
