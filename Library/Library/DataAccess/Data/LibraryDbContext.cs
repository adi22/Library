using Library.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}