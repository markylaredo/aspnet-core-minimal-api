namespace AspNetCoreWebMinimalApi.Services;

public interface ICurrentUserService
{
    public string? Username { get; }
}