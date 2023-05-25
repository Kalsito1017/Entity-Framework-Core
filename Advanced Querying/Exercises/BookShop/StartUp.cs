namespace BookShop
{
    using BookShop.Models;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Text;
    using static System.Reflection.Metadata.BlobBuilder;

    public class StartUp
    {
        public static void Main()
        {
             var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
            //int numberordate = int.Parse(Console.ReadLine());
            //string input = Console.ReadLine();
           
            db.SaveChanges();
            
        }
        //02. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var booksSorted = context.Books.OrderBy(x => x.Title).Select(x => new { x.Title, x.AgeRestriction }).ToList();
            var booksFiltered = booksSorted.Where(x => x.AgeRestriction.ToString().ToLower() == command.ToLower());
            StringBuilder sb = new StringBuilder();
            foreach (var  item  in booksFiltered)
            {
                sb.AppendLine(item.Title);
            }
            return sb.ToString().TrimEnd();
        }
        //03. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenbooks = context.Books.Where(x => x.Copies < 5000).ToList().OrderBy(X=> X.BookId);
            var truegoldenbooks = goldenbooks.Where(x => x.EditionType.ToString() == "Gold").ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in truegoldenbooks)
            {
                sb.AppendLine(item.Title);
            }
            return sb.ToString().TrimEnd();
        }
        //04. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books.Where(x => x.Price > 40).OrderByDescending( x=> x.Price);
            StringBuilder sb = new StringBuilder();
            foreach (var item in books)
            {
                sb.AppendLine($"{item.Title} - ${item.Price}");
            }
            return sb.ToString().TrimEnd();
        }
        //05. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var booksnotreleased = context.Books
                .Where(x => x.ReleaseDate.Value.Year != year)
                .OrderBy(x => x.BookId)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in booksnotreleased)
            {
                sb.AppendLine(item.Title);
            }
            return sb.ToString().TrimEnd();
        }
        //06. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(c => c.ToLower())
            .ToArray();

            string[] books = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }
        //07. Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dates = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var booksreleasedbefore = context.Books
                .Where(x => x.ReleaseDate < dates)
                .OrderByDescending(x => x.ReleaseDate.Value)
                .ToList();
               
                
            StringBuilder sb = new StringBuilder();
            foreach (var b in booksreleasedbefore)
            {
                sb.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:f2}");
            }
            return sb.ToString().TrimEnd();
        }
        //08. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(x => x.FirstName.EndsWith(input))
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in authors)
            {
                sb.AppendLine(item.FirstName + " " + item.LastName);
            }
            return sb.ToString().TrimEnd();

        }
        //09. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var booktitlescontaining = context.Books
                .Where(X => X.Title.Contains(input))
                .OrderBy(x => x.Title).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in booktitlescontaining)
            {
                sb.AppendLine(item.Title);
            }
            return sb.ToString().TrimEnd();
        }
        //10. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksbyauthor = context.Books
                .Where(x => x.Author.LastName.StartsWith(input))
                .ToList()
                .OrderBy(x=> x.BookId);
            StringBuilder sb = new StringBuilder();
            foreach (var b in booksbyauthor)
            {
                sb.AppendLine($"{b.Title} ({b.Author.FirstName + " " + b.Author.LastName})");
            }
            return sb.ToString().TrimEnd();
        }
        //11. Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var countbooks = context.Books
                .Where(x => x.Title.Length > lengthCheck)
                .ToList();
            
            return countbooks.Count;
        }
        //12. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Authors
                .Select(a => new
                {
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    TotalCopies = a.Books.Sum(x => x.Copies)
                })
                .OrderByDescending(x => x.TotalCopies)
                .ToArray();

            foreach (var book in books)
            {
                sb
                    .AppendLine($"{book.AuthorName} - {book.TotalCopies}");
            }

            return sb.ToString().TrimEnd();
        }
        //13. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var totalprofit = context.Categories
                .Select(x => new
                {
                    Name = x.Name,   
                    TotalProfit = x.CategoryBooks.Sum(x => x.Book.Price * x.Book.Copies)
                })
                .OrderByDescending(x => x.TotalProfit)
                .ThenBy(x => x.Name)
                .ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (var item in totalprofit)
            {
                sb
                    .AppendLine($"{item.Name} ${item.TotalProfit:f2}");
            }
            return sb.ToString().TrimEnd();
                
        }
        //14. Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var recentbooks = context.Categories
                .Select(x=> new
                {
                    x.Name,
                    RecentBook = x.CategoryBooks
                    .OrderByDescending(x=> x.Book.ReleaseDate)
                    .Select(b => new
                    {
                        b.Book.Title,
                        b.Book.ReleaseDate
                    })
                    .Take(3)
                })
                .OrderBy(x => x.Name)        
                .ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (var category in recentbooks)
            {
                sb.AppendLine($"--{category.Name}");

                foreach (var b in category.RecentBook)
                {
                    sb
                        .AppendLine($"{b.Title} ({b.ReleaseDate.Value.Year})");
                }
            }
            return sb.ToString().TrimEnd();
        }
        //15. Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(x => x.ReleaseDate.Value.Year < 2010);
            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }
        //16. Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var removablebooks = context.Books.Where(x => x.Copies < 4200).ToArray();
            context.RemoveRange(removablebooks);
            context.SaveChanges();
            return removablebooks.Count();
        }
    }

}



