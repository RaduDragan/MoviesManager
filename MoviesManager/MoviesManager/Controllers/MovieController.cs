using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesManager.Models;
using MoviesManager.ViewModels;
using NHibernate;
using NHibernate.Criterion;
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
        public ActionResult AddNew()
        {
            var newMovie = new MovieModel();
            
            return View(newMovie);
        }
        [HttpPost]
        public ActionResult AddNew(MovieModel model)
        {
            var newMovie = new Movie();
            using (var tr = _session.BeginTransaction())
            {
                {
                    newMovie.Id = model.Id;
                    newMovie.Name = model.Name;
                    newMovie.ReleaseDate = model.ReleaseDate;
                    _session.Save(newMovie);
                }
                tr.Commit();
            }
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
           var newMovie = new MovieModel();

            var movie = _session.Get<Movie>(id);
            if (movie != null)
            {
                newMovie.Id = movie.Id;
                newMovie.Name = movie.Name;
                newMovie.ReleaseDate = movie.ReleaseDate;
            }
            return View(newMovie);
        }

        [HttpPost]
        public ActionResult Edit(MovieModel model)
        {
            using (var tr = _session.BeginTransaction())
            {
                var movie = _session.Get<Movie>(model.Id);
                if (movie != null)
                {
                    movie.Name = model.Name;
                    movie.ReleaseDate = model.ReleaseDate;
                    _session.Save(movie);
                }
                tr.Commit();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            //...Delete
            using (var tr = _session.BeginTransaction())
            {
                var movie = _session.Get<Movie>(id);
                if (movie != null)
                {
                    _session.Delete(movie);
                }
                
                tr.Commit();
            }
            return RedirectToAction("List");
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