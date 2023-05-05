using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._MusicHub_Database.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public decimal Price
        {
            get
            {
                decimal a = 0;
                foreach (var item in Songs)
                { a += item.Price; }
                return a;
            }
        }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        [InverseProperty("Album")]
        public ICollection<Song> Songs { get; set; }
    }
}
