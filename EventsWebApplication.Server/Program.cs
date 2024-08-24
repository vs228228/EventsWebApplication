
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Application.Services;
using EventsWebApplication.Server.Domain.Interfaces;
using EventsWebApplication.Server.Infrastructure.Data;
using EventsWebApplication.Server.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

          /*  builder.Logging.ClearProviders();
            builder.Logging.AddConsole();*/

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Инъекция зависимостей
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Настройка завершена");

            app.Run();

            
        }
    }
}
