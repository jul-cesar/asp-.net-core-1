using gamesApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace gamesApi;

public static class GamesRoutes
{
    const string GetRouteName = "getName";

    public static RouteGroupBuilder MapGamesRoutes(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        //Get games endpoint
        group.MapGet(
            "/",
            async (GamesDbContext db) =>
                await db
                    .Games.Include(game => game.Genre)
                    .Select(game => game.ToDto())
                    .AsNoTracking()
                    .ToListAsync()
        );

        group
            .MapGet(
                "/{id}",
                async (int id, GamesDbContext db) =>
                {
                    Game? gameExist = await db.Games.FindAsync(id);

                    return gameExist is null
                        ? Results.NotFound()
                        : Results.Ok(gameExist.ToDetails());
                }
            )
            .WithName(GetRouteName);

        group.MapPost(
            "/",
            async (CreateGameDTO newGame, GamesDbContext db) =>
            {
                var game = newGame.ToModel();
                db.Games.Add(game);
                await db.SaveChangesAsync();

                return Results.CreatedAtRoute(GetRouteName, new { id = game.Id }, game.ToDetails());
            }
        );

        group.MapPut(
            "/{id}",
            async (int id, UpdateGameDTO newGameInfo, GamesDbContext db) =>
            {
                var gameExist = await db.Games.FindAsync(id);

                if (gameExist is null)
                {
                    return Results.NotFound();
                }

                db.Games.Entry(gameExist).CurrentValues.SetValues(newGameInfo.ToModel(id));
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
        );

        group.MapDelete(
            "/{id}",
            async (int id, GamesDbContext db) =>
            {
                await db.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
                return Results.NoContent();
            }
        );
        return group;
    }
}
