using Microsoft.EntityFrameworkCore;

namespace gamesApi;

public static class GenresRoutes
{
    public static RouteGroupBuilder MapGenresRoutes(this WebApplication app)
    {
        var group = app.MapGroup("Genres").WithParameterValidation();

        group.MapGet(
            "/",
            async (GamesDbContext db) =>
            {
               return await db.Genres.Select(genres => genres.ToDto()).AsNoTracking().ToListAsync();
            }
        );

        return group;
    }
}
