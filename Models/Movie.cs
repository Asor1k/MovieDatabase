using IMDbApiLib.Models;

namespace MovieDataBase.Models
{
    public class Movie
    {
        public Movie(long id, string imdbId,string name, string description, string imagePreview, string wikipediaUrl)
        {
            Id = id;
            IMDBId = imdbId;
            Name = name;
            Description = description;
            ImagePreview = imagePreview;
            WikipediaURL = wikipediaUrl;
        }

        public Movie(SearchResult movie, WikipediaData wikipediaData)
        {
            this.Name = movie.Title;
            this.IMDBId = movie.Id;
            this.Description = wikipediaData.PlotShort.PlainText;
            this.ImagePreview = movie.Image;
            this.WikipediaURL = wikipediaData.Url;
        }

        public Movie(Top250DataDetail movie, WikipediaData wikipediaData)
        {
            this.IMDBId = movie.Id;
            this.Name = movie.Title;
            this.Description = wikipediaData.PlotShort.PlainText;
            this.ImagePreview = movie.Image;
            this.WikipediaURL = wikipediaData.Url;
            //Movie = movie;
        }

        public long Id { get; set; }
        public string IMDBId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePreview { get; set; }
        public string WikipediaURL { get; set; }
    }
}
