using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Models;

namespace SimpleLibrary;

public class SimpleLibraryDbContext : IdentityDbContext
{
    public SimpleLibraryDbContext(DbContextOptions<SimpleLibraryDbContext> options) : base(options)
    {
    }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    public DbSet<Borrow> Borrows { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Author>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd(); 
        
        modelBuilder.Entity<ExceptionLog>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();
        
        base.OnModelCreating(modelBuilder);
    }
}