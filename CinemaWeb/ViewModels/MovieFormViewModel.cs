using CinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
