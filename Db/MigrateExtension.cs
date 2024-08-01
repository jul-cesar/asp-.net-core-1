using Microsoft.EntityFrameworkCore;

namespace gamesApi;

public static class MigrateExtension
{
    public static void MigrateDb(this WebApplication app) { 
    
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GamesDbContext>();
        dbContext.Database.Migrate();
    }
}
