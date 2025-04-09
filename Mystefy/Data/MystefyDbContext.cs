using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Models;

namespace Mystefy.Data
{
    public class MystefyDbContext : DbContext
    {
        public MystefyDbContext(DbContextOptions<MystefyDbContext> options) : base(options) { }

        public DbSet<Packaging> Packaging { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Fragrance> Fragrances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FinishedProduct> FinishedProduct { get; set; }
        public DbSet<StockRequest> StockRequests { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<WarehouseStock> WarehouseStocks { get; set; }
        public DbSet<WarehouseIngredients> WarehouseIngredients { get; set; }
        public DbSet<BatchIngredients> BatchIngredients { get; set; }
        public DbSet<Batch> Batches { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FinishedProduct>()
                .HasOne(fp => fp.Fragrance)
                .WithMany(f => f.FinishedProduct)
                .HasForeignKey(fp => fp.FragranceID);

            modelBuilder.Entity<FinishedProduct>()
                .HasOne(fp => fp.Packaging)
                .WithMany(p => p.FinishedProduct)
                .HasForeignKey(fp => fp.PackagingID);

            modelBuilder.Entity<Ingredients>()
                .HasMany(i => i.StockRequests)
                .WithOne(s => s.Ingredients)
                .HasForeignKey(s => s.IngredientsId);

            modelBuilder.Entity<BatchIngredients>()
                .HasKey(bi => new { bi.BatchID, bi.IngredientsID });


            modelBuilder.Entity<User>()
                .HasMany(u => u.StockRequests)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.StockRequests)
                .WithOne(s => s.Warehouse)
                .HasForeignKey(s => s.WarehouseId);
            modelBuilder.Entity<WarehouseStock>()
                .HasOne(ws => ws.Warehouse)
                .WithMany(w => w.WarehouseStocks)
                .HasForeignKey(ws => ws.WarehouseID);
            modelBuilder.Entity<WarehouseStock>()
                .HasOne(ws => ws.Fragrance)
                .WithMany(f => f.WarehouseStocks)
                .HasForeignKey(ws => ws.FragranceID);
        }


    }
}
