using CodeFirstDB.Data;
using CodeFirstDB.DTOs.StudentDtos;
using CodeFirstDB.Models;
using CodeFirstDB.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstDB.Endpoints.StudentEndpoints
{
    public class StudentEndpoints
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("GetAllStudent", async (UserService userService) =>
            {
                var students = await userService.GetAllStudents();

                return students;
            });

            app.MapGet("/students", async (SchoolDBContext context) =>
            {
                var studentList = await context.Students.Select(s => new StudentDto
                {
                    StudentName = s.Name,
                    StudentEmail = s.Email
                }).ToListAsync();

                return studentList;
            });

            app.MapGet("/students/{id}/profile", async (SchoolDBContext context, int id) =>
            {
                var studentList = await context.Students.Select(s => new StudentDto
                {
                    StudentName = s.Name,
                    StudentEmail = s.Email
                }).ToListAsync();

                return studentList;
            });

            app.MapGet("/students/search", async (SchoolDBContext context, string? name) =>
            {
                var studentList = await context.Students.Select(s => new StudentDto
                {
                    StudentName = s.Name,
                    StudentEmail = s.Email
                }).ToListAsync();

                if(string.IsNullOrEmpty(name))
                {
                    return studentList;
                }

                else
                {
                    studentList = studentList.Where(s => s.StudentName.Contains(name)).ToList();

                    return studentList;
                }
            });

            app.MapPost("/students", async (StudentCreateDto newStudent, SchoolDBContext context) =>
            {

                var validationContext = new ValidationContext(newStudent);
                var validationResult = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(newStudent, validationContext, validationResult, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResult.Select(v => v.ErrorMessage));
                }

                var student = new Student
                {
                    Name = newStudent.FullName,
                    Email = newStudent.EmailAddress,
                    ClassID_FK = newStudent.ClassId
                };

                context.Students.Add(student);
                await context.SaveChangesAsync();

                return Results.Ok(student);
            });

            app.MapGet("/students/{id}", async (SchoolDBContext context, int id) =>
            {
                var student = await context.Students.FirstOrDefaultAsync(s => s.ID == id);

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
        }
    }
}
