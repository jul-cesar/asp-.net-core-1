using gamesApi;
using gamesApi.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetRouteName = "getName";


List<GameDTO> games =
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


//Get games endpoint
app.MapGet("/games", () => games);



app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetRouteName);


app.MapPost("/games", (CreateGameDTO newGame) =>
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




app.Run();
