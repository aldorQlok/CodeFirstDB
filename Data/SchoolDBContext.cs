using CodeFirstDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstDB.Data
{
    public class SchoolDBContext : DbContext
    {
        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options)
        {
            
        }


        public DbSet<Student> Students { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<ClassCourse> ClassCourses { get; set; }

    }
}
