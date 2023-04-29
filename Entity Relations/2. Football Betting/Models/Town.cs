using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Football_Betting.Models
{
    public class Town
    {
        public Town()
        {
            Team = new HashSet<Team>();
        }
        [Key]
        public int TownId { get; set; }
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [InverseProperty("Town")]
        public ICollection<Team> Team { get; set; }
    }
}
