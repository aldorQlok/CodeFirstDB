
using CodeFirstDB.Data;
using CodeFirstDB.Models;
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


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/students", (SchoolDBContext context) =>
            {
                var studentList = context.Students.ToList();

                return studentList;
            });
            
            app.MapPost("/students", (Student student, SchoolDBContext context) =>
            {
                context.Students.Add(student);
                context.SaveChanges();

                return student;
            });

            app.MapGet("/students/{id}", (SchoolDBContext context, int id) =>
            {
                var student = context.Students.FirstOrDefault(s => s.ID == id);

                if (student == null)
                {
                    return Results.NotFound("Student not found");
                }

                return Results.Ok(student);
            });


            app.MapDelete("/students/{id}", (SchoolDBContext context, int id) =>
            {
                var student = context.Students.FirstOrDefault(s => s.ID == id);

                if (student == null)
                {
                    return Results.NotFound("Student not found");
                }

                context.Students.Remove(student);
                context.SaveChanges();

                return Results.Ok();
            });

            app.MapPut("/students/{id}", (SchoolDBContext context, int id, Student student) =>
            {
                var existingStudent = context.Students.FirstOrDefault(s => s.ID == id);

                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.ClassID_FK = student.ClassID_FK;

                context.SaveChanges();

                return Results.Ok();
            });

            app.Run();
        }
    }
}
