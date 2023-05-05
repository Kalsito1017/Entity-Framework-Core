using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._MusicHub_Database.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [ForeignKey("AlbumId")]
        public int AlbumId { get; set; }

        public Album Album { get; set; }
        [Required]
        [ForeignKey("WriterId")]
        public int WriterId { get; set; }
        public Writer Writer { get; set; }
        [Required]
        public decimal Price { get; set; }
        public ICollection<SongPerformer> SongPerformers { get; set; }

    }

    public enum Genre
    {
        Blues = 1,
        Rap = 2,
        PopMusic = 3,
        Rock = 4,
        Jazz = 5
    }
}
