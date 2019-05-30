using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [RoutePrefix("Movies")]
    [Route("{action = index}")]
    [Authorize]
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
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Index()
        {
            //var movies = _context.Movies.Include(e=>e.Genre).ToList();
            //return View(movies);

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
           
            return View("ReadOnlyList");
        }

        [Route("New")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                NewOrEdit = "New",                
                Genres = genres               
            };           

            return View("MovieForm",viewModel);
        }

        [Route("Edit")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                NewOrEdit="Edit",               
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [Route("Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {          
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    NewOrEdit = "Edit",
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded= DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.First(c => c.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;                
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;
            }

            _context.SaveChanges();                  

            return RedirectToAction("Index","Movies");
        }

        [Route("Details/{id}")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(e => e.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
    }
}