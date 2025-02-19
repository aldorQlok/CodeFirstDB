using System.ComponentModel.DataAnnotations;

namespace CodeFirstDB.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }

        public string Name { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<ClassCourse> ClassCourses { get; set; }
    }
}
