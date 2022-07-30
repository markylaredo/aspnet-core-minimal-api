using AspNetCoreWebMinimalApi.Models;

namespace AspNetCoreWebMinimalApi.Repositories;

public interface IUserService
{
    public User? Get(UserLogin userLogin);
}