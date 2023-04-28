using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._Student_System.Models
{
    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }
        [MaxLength(50)]
        [Unicode]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(2028)")]
        public string Url { get; set; }
        public ResourceType ResourceType { get; set; }
        public Course CourseId { get; set; }

    }
}
