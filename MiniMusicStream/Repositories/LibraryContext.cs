using Microsoft.EntityFrameworkCore;
using MiniMusicStream.Models;

namespace MiniMusicStream.Repositories
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}