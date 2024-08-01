using gamesApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace gamesApi;

public static class GamesRoutes
{
    const string GetRouteName = "getName";

    private static readonly List<GameDTO> games =
    [
        new(1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new(2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
        new(
            3,
            "The Legend of Zelda: Breath of the Wild",
            "Action-Adventure",
            59.99M,
            new DateOnly(2017, 3, 3)
        ),
        new(4, "Super Mario Odyssey", "Platformer", 49.99M, new DateOnly(2017, 10, 27)),
        new(5, "Red Dead Redemption 2", "Action-Adventure", 39.99M, new DateOnly(2018, 10, 26)),
        new(6, "The Witcher 3: Wild Hunt", "Roleplaying", 29.99M, new DateOnly(2015, 5, 19)),
        new(7, "Minecraft", "Sandbox", 26.95M, new DateOnly(2011, 11, 18))
    ];

    public static RouteGroupBuilder MapGamesRoutes(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        //Get games endpoint
        group.MapGet(
            "/",
            (GamesDbContext db) => db.Games.Include(game => game.Genre).Select(game => game.ToDto())
        );

        group
            .MapGet(
                "/{id}",
                (int id, GamesDbContext db) =>
                {
                    Game? gameExist = db.Games.Find(id);

                    return gameExist is null
                        ? Results.NotFound()
                        : Results.Ok(gameExist.ToDetails());
                }
            )
            .WithName(GetRouteName);

        group.MapPost(
            "/",
            (CreateGameDTO newGame, GamesDbContext db) =>
            {
                var game = newGame.ToModel();
                db.Games.Add(game);
                db.SaveChanges();

                return Results.CreatedAtRoute(GetRouteName, new { id = game.Id }, game.ToDetails());
            }
        );

        group.MapPut(
            "/{id}",
            (int id, UpdateGameDTO newGameInfo, GamesDbContext db) =>
            {
                var gameExist = db.Games.Find(id);

                if (gameExist is null)
                {
                    return Results.NotFound();
                }

                db.Games.Entry(gameExist).CurrentValues.SetValues(newGameInfo.ToModel(id));
                db.SaveChanges();
                return Results.NoContent();
            }
        );

        group.MapDelete(
            "/{id}",
            (int id, GamesDbContext db) =>
            {
                db.Games.Where(game => game.Id == id).ExecuteDelete();
                return Results.NoContent();
            }
        );
        return group;
    }
}
