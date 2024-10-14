using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Application.UseCases.EventUseCases;
using EventsWebApplication.Application.UseCases.UserUseCases;
using EventsWebApplication.Infrastructure.Data;
using EventsWebApplication.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using EventsWebApplication.Infrastructure.Validators;
using EventsWebApplication.Infrastructure.MappingProfile;
using EventsWebApplication.Infrastructure.Data.Infrastructure.Services;

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
            //   builder.Services.AddSwaggerGen();
            // Настройка Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Настройка JWT авторизации в Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT Bearer token into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
            });

            // маппер
            builder.Services.AddAutoMapper(typeof(UserMappingProfile));
            builder.Services.AddScoped<IMapperService, AutoMapperService>();


            // use-case
            builder.Services.AddScoped<IAddEventUseCase, AddEventUseCase>();
            builder.Services.AddScoped<ICheckUserRegisterForEventUseCase, CheckUserRegistetForEventUseCase>();
            builder.Services.AddScoped<IDeleteEventUseCase, DeleteEventUseCase>();
            builder.Services.AddScoped<IGetAllEventsUseCase, GetAllEventsUseCase>();
            builder.Services.AddScoped<IGetEventByIdUseCase, GetEventByIdUseCase>();
            builder.Services.AddScoped<IGetEventsUseCase, GetEventsUseCase>();
            builder.Services.AddScoped<IGetUsersByEventIdUseCase, GetUsersByEventIdUseCase>();
            builder.Services.AddScoped<IRegisterUserForEventUseCase, RegisterUserForEventUseCase>();
            builder.Services.AddScoped<IUnregisterUserFromEventUseCase, UnregisterUserFromEventUseCase>();
            builder.Services.AddScoped<IUpdateEventUseCase, UpdateEventUseCase>();

            builder.Services.AddScoped<IAddNotificationUseCase, AddNotificationUseCase>();
            builder.Services.AddScoped<IDeleteNotificationUseCase, DeleteNotificationUseCase>();
            builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            builder.Services.AddScoped<IGenerateAccessTokenUseCase, GenerateAccessTokenUseCase>();
            builder.Services.AddScoped<IGetAllUsersUseCase,  GetAllUsersUseCase>();
            builder.Services.AddScoped<IGetNotificationsUseCase, GetNotificationsUseCase>();
            builder.Services.AddScoped<IGetRegisteredEventsUseCase, GetRegisteredEventsUseCase>();
            builder.Services.AddScoped<IGetUserByEmailUseCase, GetUserByEmailUseCase>();
            builder.Services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
            builder.Services.AddScoped<IGetUsersUseCase, GetUsersUseCase>();
            builder.Services.AddScoped<ITryAddUserUseCase, TryAddUserUseCase>();
            builder.Services.AddScoped<ITryAuthenticateUseCase, TryAuthenticateUseCase>();
            builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ITokenManager, TokenManager>();


            // валидатор
            builder.Services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<DateOnlyDtoValidator>();
                config.RegisterValidatorsFromAssemblyContaining<UserCreateDtoValidator>();
                config.RegisterValidatorsFromAssemblyContaining<UserUpdateDtoValidator>();
                config.RegisterValidatorsFromAssemblyContaining<EventUpdateDtoValidator>();
                config.RegisterValidatorsFromAssemblyContaining<EventCreateDtoValidator>();
            });

            // jwt
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Настройка завершена");

            app.Run();

            
        }
    }
}
