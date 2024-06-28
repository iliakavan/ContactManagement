using ContactManagementV2.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementV2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany<Contact>(e => e.Contacts)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .IsRequired(false);

        //modelBuilder.Entity<Contact>()
        //    .HasOne(c => c.Category)
        //    .WithMany(c => c.Contacts)
        //    .HasForeignKey(c => c.CategoryId);
    }
}