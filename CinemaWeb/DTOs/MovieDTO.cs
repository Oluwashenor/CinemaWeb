using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
    }
}
