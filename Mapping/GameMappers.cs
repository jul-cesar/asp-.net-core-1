using gamesApi.Dtos;

namespace gamesApi;

public static class GameMappers
{
    public static Game ToModel(this CreateGameDTO game)
    {
        return new Game
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
        };
    }

    public static Game ToModel(this UpdateGameDTO game, int id)
    {
        return new Game
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
        };
    }

    public static GameDTO ToDto(this Game game)
    {
        return new(game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate);
    }

    public static GameDetailsDTO ToDetails(this Game game)
    {
        return new(game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate);
    }
}
