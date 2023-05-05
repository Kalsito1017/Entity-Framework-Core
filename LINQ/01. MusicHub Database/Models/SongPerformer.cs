using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._MusicHub_Database.Models
{
    public class SongPerformer
    {
        [ForeignKey("Song")]
        public int SongId { get; set; }
        [ForeignKey("Performer")]
        public int PerformerId { get; set; }
        [Required]
        public Song Song { get; set; }
        [Required]
        public Performer Performer { get; set; }

    }
}
