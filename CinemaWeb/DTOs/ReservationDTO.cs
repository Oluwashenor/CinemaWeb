using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Phone { get; set; }
        public DateTime ReservationTime { get; set; }
        public int MovieId { get; set; }

        public ReservationDTO()
        {
            ReservationTime = DateTime.Now;
        }
    }
}
