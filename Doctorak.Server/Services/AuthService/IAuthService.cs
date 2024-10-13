namespace Doctorak.Server.Services.AuthService;
public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<AuthResponse>> Login(string email, string password);
    Task<ServiceResponse<string>> RefreshAccessToken(string refreshToken);
    Task<ServiceResponse<string>> VerifyEmail(string email, string code);
    Task<ServiceResponse<string>> ForgotPassword(string email);
    Task<ServiceResponse<string>> VerifyPasswordReset(string email, string code);
    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
    Task<ServiceResponse<bool>> DeleteAccount(int userId);
    Task<ServiceResponse<bool>> DeleteUser(int userId);
    Task<ServiceResponse<List<User>>> FetchUsers();
}
