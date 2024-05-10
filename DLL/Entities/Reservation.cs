using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; } = new Accommodation(); 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
