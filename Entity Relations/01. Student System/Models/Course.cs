using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._Student_System.Models
{
    public class Course
    {
        public Course()
        {
            this.Homeworks = new HashSet<Homework>();
            this.Students = new HashSet<Student>();
            this.Resources = new HashSet<Resource>();
        }
        [Key]
        public int CourseId { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public ICollection<Homework> Homeworks { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Resource> Resources { get;set;}
    }
//    o CourseId
//o Name – up to 80 characters, unicode
//o   Description – unicode, not required
//o StartDate
//o EndDate
//o Price

}
