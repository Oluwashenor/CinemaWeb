using CinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.ViewModels
{
    public class ReservationFormViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public Reservation Reservation { get; set; }
    }
}
