using AspNetCoreWebMinimalApi.Models;

namespace AspNetCoreWebMinimalApi.Repositories;

public class UserService : IUserService
{
    public User? Get(UserLogin userLogin)
    {
        return UserRepository.Users.FirstOrDefault(o =>
            o.Username.Equals(userLogin.Username, StringComparison.OrdinalIgnoreCase) &&
            o.Password.Equals(userLogin.Password, StringComparison.OrdinalIgnoreCase));
    }
}