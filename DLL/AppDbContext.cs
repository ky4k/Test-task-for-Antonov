using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Налаштування зв'язків користувач-резервація(один до багатьох)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);
            // Налаштування зв'язків житло-резервація(один до багатьох)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Accommodation)
                .WithMany(a => a.Reservations)
                .HasForeignKey(r => r.AccommodationId);
        }
    }
}
