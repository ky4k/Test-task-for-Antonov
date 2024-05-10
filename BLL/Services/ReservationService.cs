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
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReservationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var reservations = await _context.Reservations.Include(r => r.User).Include(r => r.Accommodation).ToListAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto?> GetByIdAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Accommodation)
                .FirstOrDefaultAsync(r => r.Id == id);
            return reservation == null ? null : _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> CreateAsync(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> UpdateAsync(ReservationDto reservationDto)
        {
            var existingReservation = await _context.Reservations.FindAsync(reservationDto.Id);
            if (existingReservation == null)
            {
                return null;
            }

            _mapper.Map(reservationDto, existingReservation);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReservationDto>(existingReservation);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
