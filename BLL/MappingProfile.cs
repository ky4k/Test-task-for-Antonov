using AutoMapper;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Accommodation, AccommodationDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }

    }
}
