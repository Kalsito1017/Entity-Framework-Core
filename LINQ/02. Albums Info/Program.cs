
using Microsoft.EntityFrameworkCore.Internal;
using System.Text;

namespace _02._Albums_Info

{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new MusicContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var result = ExportAlbumsInfo(context, 2);
            Console.WriteLine(result);
        }
       
        public static string ExportAlbumsInfo(MusicContext context, int producerId)
        {
            var albumInfo = context.Producers
                .FirstOrDefault(x => x.Id == producerId)
                .Albums.Select(x => new
                {
                    AlbumName = x.Name,
                    ReleaseDate = x.ReleaseDate,
                    ProducerName = x.Producer.Name,
                    Songs = x.Songs.Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price,
                        Writer = s.Writer.Name,
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.Writer)
                    .ToList(),
                   AlbumPrice = x.Price
                })
                .OrderByDescending(x => x.AlbumPrice)
                .ToList();
            var sb = new StringBuilder();
            foreach (var album in albumInfo)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}")
                .AppendLine($"-ReleaseDate: {album.ReleaseDate:MM/dd/yyyy}")
                .AppendLine($"-ProducerName: {album.ProducerName}")
                .AppendLine($"-Songs:");
                int counter = 1;
                foreach (var item in album.Songs)
                {
                    sb.AppendLine($"---#{counter++}")
                        .AppendLine($"---SongName: {item.SongName}")
                        .AppendLine($"---Price: {item.Price:F2}")
                        .AppendLine($"---Writer: {item.Writer}");
                }
                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:F2}");
            }
            return sb.ToString().TrimEnd();
        }

    }
}
