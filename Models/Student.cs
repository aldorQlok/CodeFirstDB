using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstDB.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }


        [StringLength(50)]
        public string Email { get; set; }


        [ForeignKey("Class")]
        public int? ClassID_FK { get; set; }
        public virtual Class Class { get; set; }

    }
}
