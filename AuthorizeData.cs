using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreWebMinimalApi;

public class CustomAuth: IAuthorizeData
{
    public string? Policy { get; set; }
    public string? Roles { get; set; }
    public string? AuthenticationSchemes { get; set; }

    public CustomAuth(string[] roles)
    {
        Roles = string.Join(',', roles);
    }

    public CustomAuth(string roles)
    {
        Roles = roles;
    }
}