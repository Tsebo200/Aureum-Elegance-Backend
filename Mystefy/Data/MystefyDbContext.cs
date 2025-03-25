using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mystefy.Models;

namespace Mystefy.Data;

public class MystefyDbContext : DbContext
{
    public MystefyDbContext(DbContextOptions<MystefyDbContext> options) : base(options) {}
    public DbSet<Packaging> Packaging { get; set; }
    public DbSet<StockRequest> StockRequest { get; set; }
    public DbSet<Ingredients> Ingredients { get; set; }
    public DbSet<User> User { get; set; }

    //override
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ingredients>()
               .HasMany(i => i.StockRequests)
               .WithOne(s => s.Ingredients)
               .HasForeignKey(s => s.IngredientsId);

            modelBuilder.Entity<User>()
               .HasMany(u => u.StockRequests)
               .WithOne(s => s.User)
               .HasForeignKey(s => s.UserId);
    }
}
