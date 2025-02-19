using CodeFirstDB.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstDB.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        public string Name { get; set; }

        public virtual List<ClassCourse> ClassCourses { get; set; }
    }
}