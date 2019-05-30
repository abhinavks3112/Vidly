using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.DTOs;
using Vidly.Models;
using AutoMapper;

namespace Vidly.Controllers.API
{
    [Authorize]
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/movies
        [HttpGet]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult GetMovie()
        {
            var movieDto = _context.Movies
                           .Include(c=>c.Genre)
                           .ToList()
                           .Select(Mapper.Map<Movie,MovieDto>);

            return Ok(movieDto);
        }

        //GET api/movies/1
        [HttpGet]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult GetMovie(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == Id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovies(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie= Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT api/movies/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int Id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == movieDto.Id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb); 
            
            _context.SaveChanges();

            return Ok();
        }

        //DELETE api/movies/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == Id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }
    }
}
