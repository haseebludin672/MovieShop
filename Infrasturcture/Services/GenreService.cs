using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class GenreService : IGenreService
{
    private readonly IRepository<Genre> _genreRepository;

    public GenreService(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<GenreModel>> GetAllGenres()
    {
        var genres = await _genreRepository.GetAll();
        var genresModel = genres.Select(g => new GenreModel
        {
            Id = g.Id,
            Name = g.Name
        });
        return genresModel;
    }
}