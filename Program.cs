using gamesApi;

var builder = WebApplication.CreateBuilder(args);

var ConnString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GamesDbContext>(ConnString);

var app = builder.Build();


app.MapGamesRoutes();

app.MigrateDb();

app.Run();
