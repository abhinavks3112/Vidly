using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    [RoutePrefix("Movies")]
    public class MoviesController : Controller
    {
        // GET: Movies
        [Route("")]
        public ActionResult Index()
        {
            var movies = GetMovies();
            return View(movies);
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = GetMovies().SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie {Id=1, Name="Shrek"},
                new Movie{Id=2, Name="Wall-e"}
            };            
        }

    }
}