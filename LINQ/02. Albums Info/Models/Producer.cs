using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02._Albums_Info
{
    public class Producer
    {
        public Producer()
        {
            this.Albums = new HashSet<Album>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name{ get; set; }
        public string Pseudonym { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
