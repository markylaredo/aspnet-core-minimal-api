using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebMinimalApi.Data;

public static class SqlLiteDatabaseService
{
    
    public static async Task ApplySqlLiteMigrations(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();

        var provider = scope.ServiceProvider;

        var appDbContext = provider.GetRequiredService<AppDbContext>();

        var pendingMigrations = (await appDbContext.Database.GetPendingMigrationsAsync()).ToList();

        var totalPendingMigrations = pendingMigrations.Count;
        if (totalPendingMigrations > 0)
        {
            await appDbContext.Database.MigrateAsync();

        }
    }
}