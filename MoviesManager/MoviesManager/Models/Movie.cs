using System;
using FluentNHibernate.Mapping;

namespace MoviesManager.Models
{
    public class Movie
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        public virtual DateTime ReleaseDate { get; set; }
    }

    public class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Id(t => t.Id).GeneratedBy.GuidComb();
            Map(t => t.Name);
            Map(t => t.ReleaseDate);
        }
        
    }


}