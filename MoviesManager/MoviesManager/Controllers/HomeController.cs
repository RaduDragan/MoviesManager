using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesManager.Data;
using MoviesManager.Models;
using NHibernate;

namespace MoviesManager.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISession _session;

        public HomeController(ISession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            using (var tr = _session.BeginTransaction())
            {
                _session.Save(new Movie
                {
                    Name = "Movie 1"
                });

                _session.Save(new Movie
                {
                    Name = "Movie 2"
                });

                _session.Save(new Movie
                {
                    Name = "Movie 3"
                });

                tr.Commit();

            }

            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}