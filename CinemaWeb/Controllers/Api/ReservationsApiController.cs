using CinemaWeb.Data;
using CinemaWeb.DTOs;
using CinemaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var reservations = _context.Reservations.ToList();
            return Ok(reservations);
        }

        // GET api/<MoviesApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var reservations = _context.Reservations.SingleOrDefault(g => g.Id == id);
            if (reservations == null)
            {
                return NotFound();
            }
            return Ok(reservations);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReservationDTO reservationDTO)
        {
            var reservation = new Reservation
            {
                Quantity = reservationDTO.Quantity,
                Price = reservationDTO.Price,
                MovieId = reservationDTO.MovieId,
                Phone = reservationDTO.Phone
            };
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return Ok("Reservation Created Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReservationDTO reservationDTO)
        {
            var reservationInDb = _context.Reservations.SingleOrDefault(g => g.Id == id);
            if (reservationInDb == null)
            {
                return NotFound();
            };
            var reservation = new Reservation
            {
                Quantity = reservationDTO.Quantity,
                Price = reservationDTO.Price,
                MovieId = reservationDTO.MovieId,
                Phone = reservationDTO.Phone
            };
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            reservationInDb.Quantity = reservation.Quantity; 
            reservationInDb.Price = reservation.Price;
            reservationInDb.MovieId = reservation.MovieId;
            reservationInDb.Phone = reservation.Phone;
            _context.SaveChanges();
            return Ok("Updated Genre Successfully");
        }

        // DELETE api/<MoviesApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservationInDb = _context.Reservations.SingleOrDefault(r => r.Id == id);
            if (reservationInDb == null)
            {
                return NotFound();
            };
            _context.Reservations.Remove(reservationInDb);
            _context.SaveChanges();
            return Ok("Reservation Deleted succesfully");
        }
    }
}
