using _01._MusicHub_Database.Data;

namespace _01._MusicHub_Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new MusicContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}