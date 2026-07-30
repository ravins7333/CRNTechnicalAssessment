using CRN.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRN.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RefreshToken>()
        .HasOne(rt => rt.User)
        .WithMany(u => u.RefreshTokens)
        .HasForeignKey(rt => rt.UserId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Items)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId);
    }
}