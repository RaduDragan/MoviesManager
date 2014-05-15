using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesManager.Models;

namespace MoviesManager.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /List/
        public ActionResult Index()
        {
            List<ListMovies> list = new List<ListMovies>();
            return View(list);
        }
	}
}