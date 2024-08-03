using Microsoft.EntityFrameworkCore;

namespace gamesApi;

public static class MigrateExtension
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GamesDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
