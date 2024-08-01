using System.ComponentModel.DataAnnotations;

namespace gamesApi.Dtos;

public record class UpdateGameDTO(
    [Required] [StringLength(30)] string Name,
    int GenreId,
    [Range(1, 300)] decimal Price,
    DateOnly ReleaseDate
);
