using System;
using System.Data.Entity;
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
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        [Route("")]
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(e=>e.Genre).ToList();
            return View(movies);
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(e => e.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
    }
}