using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementV2.Data;

public class AppAuthDbcontext : IdentityDbContext
{
    public AppAuthDbcontext(DbContextOptions<AppAuthDbcontext> options) : base(options) 
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "USER", NormalizedName = "USER" },
            new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" }
        );
    }
}
