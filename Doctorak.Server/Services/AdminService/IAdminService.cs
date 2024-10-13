﻿namespace Doctorak.Server.Services.AdminService;

public interface IAdminService
{
    Task<ServiceResponse<int>> AdminReginster(User username, string password);
    Task<ServiceResponse<string>> AdminLogin(string username, string password);
    Task<ServiceResponse<bool>> AdminDelete(int userId);
}
