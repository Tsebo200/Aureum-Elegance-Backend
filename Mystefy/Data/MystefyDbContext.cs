using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Models;

namespace Mystefy.Data
{
    public class MystefyDbContext : DbContext
    {
        public MystefyDbContext(DbContextOptions<MystefyDbContext> options) : base(options) {}

        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Fragrance> Fragrances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FinishedProduct> FinishedProducts { get; set; }
        public DbSet<StockRequest> StockRequests { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FinishedProduct>()
                .HasOne(fp => fp.Fragrance)
                .WithMany(f => f.FinishedProducts)
                .HasForeignKey(fp => fp.FragranceID);

            modelBuilder.Entity<FinishedProduct>()
                .HasOne(fp => fp.Packaging)
                .WithMany(p => p.FinishedProducts)
                .HasForeignKey(fp => fp.PackagingID);

            modelBuilder.Entity<Ingredients>()
                .HasMany(i => i.StockRequests)
                .WithOne(s => s.Ingredients)
                .HasForeignKey(s => s.IngredientsId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.StockRequests)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.StockRequests)
                .WithOne(s => s.Warehouse)
                .HasForeignKey(s => s.WarehouseId);
        }
    }
}