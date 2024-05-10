using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Antonov.Controllers
{
    /// <summary>
    /// Контролер для управління резервуванням. 
    /// Дозволяє виконувати CRUD-операції над резервуванням.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Повертає список всіх резервувань.
        /// </summary>
        /// <returns>HTTP-відповідь зі списком усіх резервувань.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(reservations);
        }

        // <summary>
        /// Повертає резервування за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор резервування.</param>
        /// <returns>HTTP-відповідь із даними про резервування або 404, якщо не знайдено.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound(new { Message = "Reservation not found." });
            }

            return Ok(reservation);
        }

        /// <summary>
        /// Створює нове резервування.
        /// </summary>
        /// <param name="reservationDto">Дані для створення резервування.</param>
        /// <returns>HTTP-відповідь із створеним резервуванням або помилка.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationDto reservationDto)
        {
            if (reservationDto == null || reservationDto.UserId == 0 || reservationDto.AccommodationId == 0)
            {
                return BadRequest(new { Message = "Invalid reservation data." });
            }

            var createdReservation = await _reservationService.CreateAsync(reservationDto);
            return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, createdReservation);
        }

        /// <summary>
        /// Оновлює існуюче резервування.
        /// </summary>
        /// <param name="id">Ідентифікатор резервування для оновлення.</param>
        /// <param name="reservationDto">Нові дані для резервування.</param>
        /// <returns>HTTP-відповідь із 204, якщо оновлення успішне, або 400/404, якщо виникла помилка.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationDto reservationDto)
        {
            if (id != reservationDto.Id)
            {
                return BadRequest(new { Message = "Reservation ID mismatch." });
            }

            var updatedReservation = await _reservationService.UpdateAsync(reservationDto);
            if (updatedReservation == null)
            {
                return NotFound(new { Message = "Reservation not found." });
            }

            return NoContent();
        }

        // <summary>
        /// Видаляє резервування за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор резервування для видалення.</param>
        /// <returns>HTTP-відповідь із 204, якщо успішно, або 404, якщо не знайдено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var success = await _reservationService.DeleteAsync(id);
            if(success == false)
            {
                return NotFound(new { Message = "Reservation not found." });
            }
            return NoContent();
        }

    }
}
