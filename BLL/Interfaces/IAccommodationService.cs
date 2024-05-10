using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccommodationService
    {
        Task<IEnumerable<AccommodationDto>> GetAllAsync();
        Task<AccommodationDto> GetByIdAsync(int id);
        Task<AccommodationDto> CreateAsync(AccommodationDto accommodationDto);
        Task<AccommodationDto> UpdateAsync(AccommodationDto accommodationDto);
        Task<bool> DeleteAsync(int id);
    }
}
