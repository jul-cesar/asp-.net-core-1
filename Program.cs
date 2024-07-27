using gamesApi;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGamesRoutes();

app.Run();
