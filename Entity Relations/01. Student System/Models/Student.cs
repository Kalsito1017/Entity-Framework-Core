using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._Student_System.Models
{
    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
            this.Homeworks = new HashSet<Homework>();
        }
        [Key]
        public int StudentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Column(TypeName ="char(10)")]
        public int PhoneNumber { get; set; }
        public DateTime RegisterOn { get; set; }
        public DateTime? Birthday { get; set; }

        public ICollection<Homework> Homeworks { get; set; }

        public ICollection<Course> Courses { get; set; }

        
    }
}
