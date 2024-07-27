using gamesApi.Dtos;

namespace gamesApi;

public static class GamesRoutes
{
    const string GetRouteName = "getName";


    private static readonly List<GameDTO> games =
    [
        new
    (
         1,
         "Street Fighter II",
       "Fighting",
        19.99M,
       new DateOnly(1992, 7, 15)
    ),
    new
    (
       2,
         "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)
   )    ,
    new
    (
       3,
        "The Legend of Zelda: Breath of the Wild",
     "Action-Adventure",
      59.99M,
        new DateOnly(2017, 3, 3)
    ),
    new
    (
      4,
        "Super Mario Odyssey",
      "Platformer",
         49.99M,
         new DateOnly(2017, 10, 27)
    ),
    new
    (
         5,
       "Red Dead Redemption 2",
         "Action-Adventure",
     39.99M,
       new DateOnly(2018, 10, 26)
    ),
    new
    (
        6,
         "The Witcher 3: Wild Hunt",
      "Roleplaying",
       29.99M,
         new DateOnly(2015, 5, 19)
    ),
    new
    (
      7,
        "Minecraft",
        "Sandbox",
        26.95M,
        new DateOnly(2011, 11, 18)
    )

    ];
    public static RouteGroupBuilder MapGamesRoutes(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        //Get games endpoint
        group.MapGet("/", () => games);



        group.MapGet("/{id}", (int id) =>
        {

            int gameExist = games.FindIndex(game => game.Id == id);
            if (gameExist == -1)
            {
                return Results.NotFound();
            }
            var game = games.Find(game => game.Id == id);

            return Results.Ok(game);


        }).WithName(GetRouteName);


        group.MapPost("/", (CreateGameDTO newGame) =>
        {
            var game = new GameDTO(
        Id: games.Count + 1,
        Name: newGame.Name,
        Genre: newGame.Genre,
        Price: newGame.Price,
        ReleaseDate: newGame.ReleaseDate
    );
            games.Add(game);

            return Results.CreatedAtRoute(GetRouteName, new { id = game.Id }, game);
        });


        group.MapPut("/{id}", (int id, UpdateGameDTO newGameInfo) =>
        {
            int index = games.FindIndex(game => game.Id == id);
            games[index] = new GameDTO(
      id,
      Name: newGameInfo.Name,
      Price: newGameInfo.Price,
      Genre: newGameInfo.Genre,
      ReleaseDate: newGameInfo.ReleaseDate
    );
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        return group;
    }
}
