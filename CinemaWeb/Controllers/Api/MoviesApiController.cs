using CinemaWeb.Data;
using CinemaWeb.DTOs;
using CinemaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CinemaWeb.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MoviesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<MoviesApiController>
        [HttpGet]
        public IActionResult Get()
        {
            var movies = _context.Movies.ToList();
            return Ok(movies);
        }

        // GET api/<MoviesApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetPrice(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie.Price);
        }

        // POST api/<MoviesApiController>
        [HttpPost]
        public IActionResult Post([FromBody] MovieDTO movieDTO)
        {
            var movie = new Movie
            {
                Name = movieDTO.Name,
                Price = movieDTO.Price,
                Language = movieDTO.Language,
                Rating = movieDTO.Rating,
                GenreId = movieDTO.GenreId
            };
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Ok("Movie Created Succesfully");
        }

        // PUT api/<MoviesApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MovieDTO movieDTO)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            };
            var movie = new Movie
            {
                Name = movieDTO.Name,
                Price = movieDTO.Price,
                Language = movieDTO.Language,
                Rating = movieDTO.Rating,
                GenreId = movieDTO.GenreId
            };

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            movieInDb.Name = movie.Name;
            movieInDb.Price = movie.Price;
            movieInDb.Language = movie.Language;
            movieInDb.Rating = movie.Rating;
            movieInDb.GenreId = movie.GenreId;
            _context.SaveChanges();
            return Ok("Movie updated Succefully");
        }

        // DELETE api/<MoviesApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(g => g.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            };
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok("Movie Deleted succesfully");
        }
    }
}
