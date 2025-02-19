using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstDB.Models
{
    public class ClassCourse
    {
        [Key]
        public int ClassCourseID { get; set; }

        [ForeignKey("Class")]
        public int ClassID_FK { get; set; }
        public virtual Class Class { get; set; }

        [ForeignKey("Course")]
        public int CourseID_FK { get; set; }
        public virtual Course Course { get; set; }

    }
}
