using Microsoft.AspNetCore.Authorization;

public class CustomAuthorizeData : IAuthorizeData
{
    public string? Policy { get; set; }
    public string? Roles { get; set; }
    public string? AuthenticationSchemes { get; set; }

    public CustomAuthorizeData(string[] roles)
    {
        Roles = string.Join(',', roles);
    }

    public CustomAuthorizeData(string roles)
    {
        Roles = roles;
    }
}