using FluentValidation;

namespace AspNetCoreWebMinimalApi.Models;

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class UserLoginValidator : AbstractValidator<UserLogin>
{
    public UserLoginValidator()
    {
        RuleFor(d => d.Username).NotEmpty();
        RuleFor(d => d.Password).NotEmpty();
    }
}