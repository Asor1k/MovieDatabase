using Microsoft.AspNetCore.Mvc;
using MovieDataBase.Interfaces;
using MovieDataBase.Models;
using MovieDataBase.Repositories;

namespace MovieDataBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieRepository movieRepository;

        public MoviesController()
        {
            movieRepository = new MovieRepository();
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            var movies = movieRepository.GetAllMovies();

            if (movies == null)
            {
                return NotFound();
            }

            return movies;
        }

        // GET: api/Movies/title
        [HttpGet("{title}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByTitle(string title)
        {
            var movies = await movieRepository.GetMoviesTitleMatching(title);

            if (movies == null)
            {
                return NotFound();
            }

            if (movies.Count == 0)
            {
                return NotFound();
            }

            return movies;
        }
    }
}
