using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<ReservationDto> GetByIdAsync(int id);
        Task<ReservationDto> CreateAsync(ReservationDto reservationDto);
        Task<ReservationDto> UpdateAsync(ReservationDto reservationDto);
        Task<bool> DeleteAsync(int id);
    }
}
