using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesManager.Models;
using MoviesManager.ViewModels;
using NHibernate;
using NHibernate.Linq;

namespace MoviesManager.Controllers
{
    public class MovieController : Controller
    {
        private readonly ISession _session;

        public MovieController(ISession session)
        {
            _session = session;
        }

        //
        // GET: /Movie/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult List()
        {
            //var ml = _session.Query<Movie>().Select(m=>new MovieModel
            //{
            //    Id = m.Id,
            //    Name = m.Name,
            //    ReleaseDate = m.ReleaseDate
            //}).ToList();

            //var model = new MoviesList();
            //model.Movies = ml;

            var moviesList = new MoviesList();
            moviesList.Movies = new List<MovieModel>();
            var movies = _session.Query<Movie>().ToList();
            foreach (var movie in movies)
            {
                moviesList.Movies.Add(new MovieModel()
                {
                    Name = movie.Name,
                    ReleaseDate = movie.ReleaseDate,
                    Id = movie.Id

                });
            }
            return View(moviesList);
        }
	}
}