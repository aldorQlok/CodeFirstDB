
using CodeFirstDB.Data;
using CodeFirstDB.DTOs.StudentDtos;
using CodeFirstDB.Endpoints.StudentEndpoints;
using CodeFirstDB.Models;
using CodeFirstDB.Services;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SchoolDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                Console.WriteLine("Request Recieved.");
                string? configuredApiKey = builder.Configuration["ApiKey"];
                var apiKey = context.Request.Headers["X-API-Key"].FirstOrDefault();

                if (apiKey != configuredApiKey)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Du får inte tillgång!");
                    return;
                }

                await next(context);

            });

            StudentEndpoints.RegisterEndpoints(app);

            app.Run();
        }
    }
}
