using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Phone { get; set; }
        public DateTime ReservationTime { get; set; }
        [Display(Name="Movie")]
        public int MovieId { get; set; }
        public bool Deleted { get; set; }

        public Reservation()
        {
            Deleted = false;
        }
    }
}
