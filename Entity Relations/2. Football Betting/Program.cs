using _2._Football_Betting.Data;

namespace _2._Football_Betting
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new FootballBettingContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}