using _01._Student_System.Data;

namespace _01._Student_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new StudentSystemContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}