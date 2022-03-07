using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public double Rating { get; set; }
        public Genre Genre { get; set; }
        [Required]
        [Display(Name="Genre")]
        public int GenreId { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
