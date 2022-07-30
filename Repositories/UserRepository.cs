using AspNetCoreWebMinimalApi.Models;
using AspNetCoreWebMinimalApi.Services;

namespace AspNetCoreWebMinimalApi.Repositories;

public static class UserRepository
{
    public static List<User> Users => new()
    {
        new()
        {
            Username = "admin", Password = "aabc123",
            Role = AppConstants.Role.Admin
        },
        new()
        {
            Username = "user", Password = "user123",
            Role = AppConstants.Role.User
        },
    };
}