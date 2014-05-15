using System;

namespace MoviesManager.ViewModels
{
    public class MovieModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}