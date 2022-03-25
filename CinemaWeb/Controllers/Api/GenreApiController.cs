using CinemaWeb.Data;
using CinemaWeb.DTOs;
using CinemaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Controllers.Api
{
    [Route("api/genres")]
    [ApiController]
    public class GenreApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenreApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var genres = _context.Genres.ToList();
            return Ok(genres);
        }

        // GET api/<MoviesApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GenreDTO genreDTO)
        {
            var genre = new Genre
            {
                Name = genreDTO.Name,
            };
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return Ok("Genre Created Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]GenreDTO genreDTO)
        {
            var genreInDb = _context.Genres.SingleOrDefault(g => g.Id == id);
            if (genreInDb == null)
            {
                return NotFound();
            };
            var genre = new Genre
            {
                Name = genreDTO.Name,
            };
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            genreInDb.Name = genre.Name;
            _context.SaveChanges();
            return Ok("Updated Genre Successfully");
        }

        // DELETE api/<MoviesApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var genreInDb = _context.Genres.SingleOrDefault(g => g.Id == id);
            if (genreInDb == null)
            {
                return NotFound();
            };
            _context.Genres.Remove(genreInDb);
            _context.SaveChanges();
            return Ok("Genre Deleted succesfully");
        }
    }
}
