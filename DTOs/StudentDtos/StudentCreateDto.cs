using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstDB.DTOs.StudentDtos
{
    public class StudentCreateDto
    {
        [MinLength(5)]
        [MaxLength(50)]
        public string FullName { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public int ClassId { get; set; }
    }
}
