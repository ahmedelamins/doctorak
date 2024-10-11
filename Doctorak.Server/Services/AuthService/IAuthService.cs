namespace Doctorak.Server.Services.AuthService;
public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<string>> Login(string email, string password);
    Task<ServiceResponse<string>> VerifyEmail(string email, string code);
    Task<ServiceResponse<string>> ForgotPassword(string email);
    Task<ServiceResponse<string>> VerifyPasswordReset(string email, string code);
    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
    Task<ServiceResponse<bool>> DeleteUser(int userId);
}
