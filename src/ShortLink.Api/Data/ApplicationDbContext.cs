using Microsoft.EntityFrameworkCore;

namespace ShortLink.Api.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UrlMapper> UrlMappers => Set<UrlMapper>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UrlMapper>().HasKey(e => e.OriginalUrl);
        modelBuilder.Entity<UrlMapper>().HasKey(e => e.Guid);

        modelBuilder.Entity<UrlMapper>().Property(e => e.OriginalUrl).IsRequired();
        modelBuilder.Entity<UrlMapper>().Property(e => e.Guid).IsRequired();
    }
}
