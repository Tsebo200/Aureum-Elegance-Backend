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
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryIngredients> DeliveryIngredients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FinishedProduct> FinishedProduct { get; set; }
        public DbSet<StockRequest> StockRequests { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<WarehouseStock> WarehouseStocks { get; set; }
        public DbSet<WarehouseIngredients> WarehouseIngredients { get; set; }
        public DbSet<BatchFinishedProduct> BatchFinishedProducts { get; set; }
        public DbSet<BatchIngredients> BatchIngredients { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<FragranceIngredient> FragranceIngredients { get; set; }
        public DbSet<StockRequestIngredients> StockRequestIngredients { get; set; }
        public DbSet<StockRequestPackagings> StockRequestPackagings { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<WasteLossRecordIngredients> WasteLossRecordIngredients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ingredients>()
                .HasMany(i => i.WasteLossRecordIngredients)
                .WithOne(wlri => wlri.Ingredients)
                .HasForeignKey(wlri => wlri.IngredientsId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.WasteLossRecordIngredients)
                .WithOne(wlri => wlri.User)
                .HasForeignKey(wlri => wlri.UserId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.WasteLossRecordIngredients)
                .WithOne(wlri => wlri.Warehouse)
                .HasForeignKey(wlri => wlri.WarehouseId);


            modelBuilder.Entity<Packaging>()
                .HasMany(p => p.StockRequestPackagings)
                .WithOne(srp => srp.Packaging)
                .HasForeignKey(srp => srp.PackagingId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.StockRequestPackagings)
                .WithOne(srp => srp.User)
                .HasForeignKey(srp => srp.UserId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.StockRequestPackagings)
                .WithOne(srp => srp.Warehouse)
                .HasForeignKey(srp => srp.WarehouseId);

            modelBuilder.Entity<Ingredients>()
                .HasMany(i => i.StockRequestIngredients)
                .WithOne(sri => sri.Ingredients)
                .HasForeignKey(sri => sri.IngredientsId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.StockRequestIngredients)
                .WithOne(sri => sri.User)
                .HasForeignKey(sri => sri.UserId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.StockRequestIngredients)
                .WithOne(sri => sri.Warehouse)
                .HasForeignKey(sri => sri.WarehouseId);

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

            modelBuilder.Entity<BatchFinishedProduct>()
               .HasKey(bfp => new { bfp.BatchID, bfp.ProductID });

            modelBuilder.Entity<FragranceIngredient>()
            .HasKey(fi => new { fi.FragranceID, fi.IngredientsID });

            modelBuilder.Entity<FragranceIngredient>()
                .HasOne(fi => fi.Fragrance)
                .WithMany(f => f.FragranceIngredients)
                .HasForeignKey(fi => fi.FragranceID);

            modelBuilder.Entity<FragranceIngredient>()
                .HasOne(fi => fi.Ingredients)
                .WithMany(i => i.FragranceIngredients)
                .HasForeignKey(fi => fi.IngredientsID);

            modelBuilder.Entity<Supplier>()
               .HasKey(s => s.SupplierID);
        }


    }
}
