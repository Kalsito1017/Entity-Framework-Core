using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Football_Betting.Models
{
    public class Position
    {
        public Position()
        {
            Players = new HashSet<Player>();
        }
        [Key]
        public int PositionId { get; set; }
        public bool Name { get; set; }
        [InverseProperty("Position")]
        public ICollection<Player> Players { get; set; }
    }
}
