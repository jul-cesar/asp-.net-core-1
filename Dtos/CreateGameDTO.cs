using System.ComponentModel.DataAnnotations;

namespace gamesApi;

public record class CreateGameDTO(
    [Required][StringLength(30)] string Name,
    int GenreId,
    [Range(1, 300)] decimal Price,
    DateOnly ReleaseDate
    );
