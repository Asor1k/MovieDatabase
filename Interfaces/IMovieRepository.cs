using MovieDataBase.Models;

namespace MovieDataBase.Interfaces
{
    public interface IMovieRepository
    {
        public List<Movie> GetAllMovies();
        public Task<List<Movie>> GetMoviesTitleMatching(string title);
    }
}
