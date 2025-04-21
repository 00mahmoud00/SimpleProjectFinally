using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Models;

namespace SimpleLibrary;

public class SimpleLibraryDbContext : DbContext
{
    public SimpleLibraryDbContext(DbContextOptions<SimpleLibraryDbContext> options) : base(options)
    {
    }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Author>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();
        
        base.OnModelCreating(modelBuilder);
    }
}