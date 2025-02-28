using System.ComponentModel.DataAnnotations;

namespace CodeFirstDB.DTOs.StudentDtos
{
    public class StudentDto
    {
        [MinLength(5)]
        [MaxLength(50)]
        public string StudentName { get; set; }

        [EmailAddress]
        public string StudentEmail { get; set; }
    }
}
