namespace AspNetCoreWebMinimalApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public CurrentUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public string? Username => _httpContext.HttpContext?.User.Identity?.Name;
    public bool IsAdmin => _httpContext.HttpContext?.User.IsInRole(AppConstants.Role.Admin) ?? false;
}

public static class AppConstants
{
    public static class Role
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
    }
}