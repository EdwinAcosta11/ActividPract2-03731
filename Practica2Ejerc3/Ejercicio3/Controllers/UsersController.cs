using Microsoft.AspNetCore.Mvc;
using UserAuthAPI.Models;
using UserAuthAPI.Services;

namespace UserAuthAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        // POST: api/users/register
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Todos los campos son requeridos.");

            if (!UserService.IsValidEmail(user.Email))
                return BadRequest("El email no tiene formato válido.");

            if (user.Password.Length < 8)
                return BadRequest("La contraseña debe tener al menos 8 caracteres.");

            if (UserService.Users.Any(u => u.Email == user.Email))
                return BadRequest("El usuario ya está registrado.");

            user.Id = UserService.Users.Count + 1;
            UserService.Users.Add(user);
            return Ok("Usuario registrado exitosamente.");
        }

        // POST: api/users/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = UserService.Users.FirstOrDefault(
                u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
                return Unauthorized("Credenciales incorrectas.");

            var token = UserService.GenerateToken(user.Email);
            UserService.Tokens[user.Email] = token;

            return Ok(new { Token = token });
        }

        // GET opcional: ver usuarios registrados (solo para pruebas)
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(UserService.Users);
        }
    }
}

