namespace gamesApi;

public static class GenreMappers
{
    public static GenreDTO ToDto(this Genre genre)
    {
        return new(genre.Id, genre.Name);
    }
}
