using CodeFirstDB.Data;
using CodeFirstDB.DTOs.StudentDtos;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstDB.Services
{
    public class UserService
    {
        private readonly SchoolDBContext context;

        public UserService(SchoolDBContext _context)
        {
            context = _context;
        }

        public async Task<List<StudentDto>> GetAllStudents()
        {
            var studentList = await context.Students.Select(s => new StudentDto
            {
                StudentName = s.Name,
                StudentEmail = s.Email
            }).ToListAsync();

            return studentList;
        }
    }
}
