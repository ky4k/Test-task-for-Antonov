using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Antonov.Controllers
{
    /// <summary>
    /// Контролер для управління місцями розміщення (accommodations). 
    /// Дозволяє виконувати CRUD-операції з місцями розміщення.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccommodationController : ControllerBase
    {
        private readonly IAccommodationService _accommodationService;

        public AccommodationController(IAccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
        }

        /// <summary>
        /// Повертає всі місця розміщення.
        /// </summary>
        /// <returns>HTTP-відповідь з переліком усіх місць розміщення.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAccommodations()
        {
            var accommodations = await _accommodationService.GetAllAsync();
            return Ok(accommodations);
        }

        /// <summary>
        /// Повертає місце розміщення за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор місця розміщення.</param>
        /// <returns>HTTP-відповідь з даними місця розміщення або 404, якщо не знайдено.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccommodationById(int id)
        {
            var accommodation = await _accommodationService.GetByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound(new { Message = "Accommodation not found." });
            }

            return Ok(accommodation);
        }

        /// <summary>
        /// Створює нове місце розміщення.
        /// </summary>
        /// <param name="accommodationDto">Дані для створення місця розміщення.</param>
        /// <returns>HTTP-відповідь із новим місцем розміщення або помилка.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccommodation(AccommodationDto accommodationDto)
        {
            if (accommodationDto == null || string.IsNullOrEmpty(accommodationDto.Name))
            {
                return BadRequest(new { Message = "Invalid accommodation data." });
            }
            var createdAccommodation = await _accommodationService.CreateAsync(accommodationDto);
            return CreatedAtAction(nameof(GetAccommodationById), new { id = createdAccommodation.Id }, createdAccommodation);
        }

        /// <summary>
        /// Оновлює існуюче місце розміщення.
        /// </summary>
        /// <param name="id">Ідентифікатор місця розміщення для оновлення.</param>
        /// <param name="accommodationDto">Нові дані для місця розміщення.</param>
        /// <returns>HTTP-відповідь із 204, якщо оновлення успішне, або 400/404, якщо виникла помилка.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccommodation(int id, AccommodationDto accommodationDto)
        {
            if (id != accommodationDto.Id)
            {
                return BadRequest(new { Message = "Accommodation ID mismatch." });
            }

            var updatedAccommodation = await _accommodationService.UpdateAsync(accommodationDto);
            if (updatedAccommodation == null)
            {
                return NotFound(new { Message = "Accommodation not found." });
            }

            return NoContent();
        }

        // <summary>
        /// Видаляє місце розміщення за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор місця розміщення для видалення.</param>
        /// <returns>HTTP-відповідь із 204, якщо видалення успішне, або 404, якщо не знайдено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccommodation(int id)
        {
            var success = await _accommodationService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new { Message = "Accommodation not found." });
            }

            return NoContent();
        }
    }
}
