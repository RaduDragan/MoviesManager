using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesManager.Models
{
    public class ListMovies
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        public virtual DateTime ReleaseDate { get; set; }
    }
}