using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Calabonga.Catalog2023.Infrastructure;

/// <summary>
/// Database context for current application
/// </summary>
public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<EventItem> EventItems { get; set; }

    public DbSet<ApplicationUserProfile> Profiles { get; set; }

    public DbSet<AppPermission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.UseOpenIddict<Guid>();
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasQueryFilter(x => x.Visible);
        builder.Entity<Review>().HasQueryFilter(x => x.Visible);
        builder.Entity<Product>().HasQueryFilter(x => x.Visible);
    }
}

/// <summary>
/// ATTENTION!
/// It should uncomment two line below when using real Database (not in memory mode). Don't forget update connection string.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql("User ID=sa;Password=8jkGh47hnDw89Haq8LN2;Host=localhost;Port=5432;Database=CatalogDb2023;");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}