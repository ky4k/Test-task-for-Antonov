using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Antonov.Controllers
{
    /// <summary>
    /// Контролер для управління користувачами. 
    /// Дозволяє виконувати CRUD-операції над користувачами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Повертає список всіх користувачів.
        /// </summary>
        /// <returns>HTTP-відповідь з списком всіх користувачів.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        /// <summary>
        /// Повертає користувача за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор користувача.</param>
        /// <returns>HTTP-відповідь з даними користувача або 404, якщо не знайдено.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            return Ok(user);
        }
        /// <summary>
        /// Створює нового користувача.
        /// </summary>
        /// <param name="userDto">Дані користувача для створення.</param>
        /// <returns>HTTP-відповідь з створеним користувачем або помилка.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email))
            {
                return BadRequest(new { Message = "Invalid user data." });
            }

            var createdUser = await _userService.CreateAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        /// <summary>
        /// Оновлює існуючого користувача.
        /// </summary>
        /// <param name="id">Ідентифікатор користувача для оновлення.</param>
        /// <param name="userDto">Нові дані користувача.</param>
        /// <returns>HTTP-відповідь з 204, якщо оновлення успішне, або 400/404, якщо виникла помилка.</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest(new { Message = "User ID mismatch." });
            }

            var updatedUser = await _userService.UpdateAsync(userDto);
            if (updatedUser == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            return NoContent();
        }
        /// <summary>
        /// Видаляє користувача за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор користувача для видалення.</param>
        /// <returns>HTTP-відповідь з 204, якщо успішно, або 404, якщо не знайдено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new { Message = "User not found." });
            }
            return NoContent();
        }
    }
}
