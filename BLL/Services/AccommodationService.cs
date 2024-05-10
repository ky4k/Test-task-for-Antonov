using BLL.Interfaces;
using BLL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace BLL.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AccommodationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccommodationDto>> GetAllAsync()
        {
            var accommodations = await _context.Accommodations.ToListAsync();
            return _mapper.Map<IEnumerable<AccommodationDto>>(accommodations);
        }

        public async Task<AccommodationDto?> GetByIdAsync(int id)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);
            return accommodation == null ? null : _mapper.Map<AccommodationDto>(accommodation);
        }

        public async Task<AccommodationDto> CreateAsync(AccommodationDto accommodationDto)
        {
            var accommodation = _mapper.Map<Accommodation>(accommodationDto);
            _context.Accommodations.Add(accommodation);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccommodationDto>(accommodation);
        }

        public async Task<AccommodationDto> UpdateAsync(AccommodationDto accommodationDto)
        {
            var existingAccommodation = await _context.Accommodations.FindAsync(accommodationDto.Id);
            if (existingAccommodation == null)
            {
                return null;
            }

            _mapper.Map(accommodationDto, existingAccommodation);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccommodationDto>(existingAccommodation);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);
            if (accommodation == null)
            {
                return false;
            }

            _context.Accommodations.Remove(accommodation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
