using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.ViewModels
{
    public class RegisterViewModel
    {


        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password does not match ")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role { get; set; }
        public RegisterViewModel()
        {
            Role = "User";
        }
    }
}
