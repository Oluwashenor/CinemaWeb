using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using CinemaWeb.Data;
using CinemaWeb.Models;
using AuthenticationPlugin;
using Microsoft.AspNetCore.Identity;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly AuthService _auth;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(UserManager<IdentityUser> userManager, ApplicationDbContext Context, IConfiguration configuration,  RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _context = Context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var userWithSameEmail = _context.Users.Where(u => u.Email == model.Email).SingleOrDefault();
            if (userWithSameEmail != null)
            {
                return BadRequest("User with same email already Exist");
            }
            var user = new ApplicationUser()
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                NormalizedEmail = model.Email.ToUpper(),
                PasswordHash = SecurePasswordHasherHelper.Hash(model.Password)
            };
            //var result = await _userManager.CreateAsync(user, model.Password);
            _context.Users.Add(user);
             var result = await _context.SaveChangesAsync();

            if (result.Equals(0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _userManager.AddToRoleAsync(user, "User");
            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var userInDb = _context.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userInDb == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (!SecurePasswordHasherHelper.Verify(user.Password, userInDb.PasswordHash))
            {
                return Unauthorized();
            }
            var userRole = await _userManager.GetRolesAsync(userInDb);
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(ClaimTypes.Email,user.Email),
               new Claim(ClaimTypes.Role,userRole.First())
             };
            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id = userInDb.Id
            });
        }


        [HttpPost]
        public IActionResult ChangePassword([FromForm] Password password)
        {
            var user = (_context.Users.Where(u => u.Email == password.Email)).FirstOrDefault();
            if (user == null)
                return NotFound();
            if (!SecurePasswordHasherHelper.Verify(password.OldPassword, user.PasswordHash))
            {
                return BadRequest("Old Password not Correct");
            }
            if (password.NewPassword != password.ConfirmPassword)
            {
                return BadRequest("New Password and Confirm Password does not Match");
            }
            user.PasswordHash = SecurePasswordHasherHelper.Hash(password.ConfirmPassword);
            _context.SaveChanges();
            return Ok("Password Updated Successfully");

        }

    }
}
