
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using Microsoft.EntityFrameworkCore;


namespace Antonov
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Конфігурація сервісів
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Конфігурація бази даних
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // AutoMapper і профіль маппінгу
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //DI
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccommodationService, AccommodationService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();

            var app = builder.Build();

            // Конфігурація HTTP запитів
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Antonov API v1");
                    c.RoutePrefix = string.Empty; 
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync(); 
        }
    }
}
