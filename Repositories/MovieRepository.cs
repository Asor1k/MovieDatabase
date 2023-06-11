using IMDbApiLib;
using IMDbApiLib.Models;
using MovieDataBase.Interfaces;
using MovieDataBase.Models;

namespace MovieDataBase.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private const string AmdbAccessKey = "k_7hm71c3g";
        private const int AmountOfMoviesInFirstFetch = 20;
        private List<Movie> allMovies;

        public MovieRepository() 
        {
            //Get movies from api
            allMovies = FetchMoviesFromIMDBToMovieContext().Result;
        }

        private async Task<List<Movie>> FetchMoviesFromIMDBToMovieContext()
        {
            ApiLib apiLib = new ApiLib(AmdbAccessKey);
            var fetchedMoviesList = await apiLib.Top250MoviesAsync();
            List<Movie> movies = new List<Movie>();
            
            for (int i = 0; i < AmountOfMoviesInFirstFetch; i++)
            {
                Top250DataDetail movie = fetchedMoviesList.Items[i];
                WikipediaData wikipediaData = await apiLib.WikipediaAsync(movie.Id, IMDbApiLib.Models.Language.en);
                movies.Add(new Movie(movie, wikipediaData));
            }
            return movies;
        }

        public async Task<List<Movie>> GetMoviesTitleMatching(string title)
        {
            List<Movie> moviesMatching = allMovies.Where(movie => movie.Name.Contains(title, StringComparison.CurrentCultureIgnoreCase)).ToList();
            if(moviesMatching.Count > 0)
            {
                return moviesMatching;
            }

            ApiLib apiLib = new ApiLib(AmdbAccessKey);
            var searchResuts = (await apiLib.SearchMovieAsync(title)).Results;
            searchResuts.ForEach(res => Console.WriteLine(res.ResultType));
            searchResuts.ForEach(async searchResult =>
            {
                WikipediaData wikipediaData = await apiLib.WikipediaAsync(searchResult.Id, IMDbApiLib.Models.Language.en);
                moviesMatching.Add(new Movie(searchResult, wikipediaData));
            });
            
            //Cache movies found that are not in previously fetched movies for faster work
            moviesMatching.ForEach(matchingMovie =>
            {
                if(!allMovies.Exists(movie => movie.Name == matchingMovie.Name))
                {
                    allMovies.Add(matchingMovie);
                }
            });

            return moviesMatching;
        }

        public List<Movie> GetAllMovies()
        {
            return allMovies;
        }
    }
}
