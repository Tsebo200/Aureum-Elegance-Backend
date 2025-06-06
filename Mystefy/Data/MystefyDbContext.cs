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
        public DbSet<WasteLossRecordPackaging> WasteLossRecordPackaging { get; set;}
        public DbSet <WasteLossRecordFragrance> WasteLossRecordFragrance { get; set;}
        public DbSet <WasteLossRecordBatchFinishedProducts> WasteLossRecordBatchFinishedProducts { get; set;}
        public DbSet<FinishedProductPackaging> FinishedProductPackaging { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // WasteLossRecordBatchFinishedProducts
            modelBuilder.Entity<User>()
                .HasMany(u => u.WasteLossRecordBatchFinishedProducts)
                .WithOne(wlrbfp => wlrbfp.User)
                .HasForeignKey(wlrbfp => wlrbfp.UserId);

            modelBuilder.Entity<FinishedProduct>()
                .HasMany(fp => fp.WasteLossRecordBatchFinishedProducts)
                .WithOne(wlrbfp => wlrbfp.FinishedProduct)
                .HasForeignKey(wlrbfp => wlrbfp.ProductId)
                // Need to tell EF the name of constraint as EF would surpass the character limit when creating the name for the constraint
                // This is the problem below...Produc~ is supposed to be ProductId but the character limit would be exceeded 
                // MIGRATION SNIPPET name: "FK_WasteLossRecordBatchFinishedProducts_FinishedProduct_Produc~"
                .HasConstraintName("FK_WLRBatchFinishedProducts_FinishedProduct_ProductId");

            modelBuilder.Entity<Batch>()
                .HasMany(f => f.WasteLossRecordBatchFinishedProducts)
                .WithOne(wlrbfp => wlrbfp.Batch)
                .HasForeignKey(wlrbfp => wlrbfp.BatchId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.WasteLossRecordBatchFinishedProducts)
                .WithOne(wlrbfp => wlrbfp.Warehouse)
                .HasForeignKey(wlrbfp => wlrbfp.WarehouseId);


            // WasteLossRecordFragrance
            modelBuilder.Entity<User>()
                .HasMany(u => u.WasteLossRecordFragrance)
                .WithOne(wlrf => wlrf.User)
                .HasForeignKey(wlrf => wlrf.UserId);

            modelBuilder.Entity<Fragrance>()
                .HasMany(f => f.WasteLossRecordFragrance)
                .WithOne(wlrf => wlrf.Fragrance)
                .HasForeignKey(wlrf => wlrf.FragranceId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.WasteLossRecordFragrance)
                .WithOne(wlrf => wlrf.Warehouse)
                .HasForeignKey(wlrf => wlrf.WarehouseId);

            // WasteLossRecordPackaging
            modelBuilder.Entity<User>()
                .HasMany(u => u.WasteLossRecordPackaging)
                .WithOne(wlrp => wlrp.User)
                .HasForeignKey(wlrp => wlrp.UserId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.WasteLossRecordPackaging)
                .WithOne(wlrp => wlrp.Warehouse)
                .HasForeignKey(wlrp => wlrp.WarehouseId);

            modelBuilder.Entity<Packaging>()
                .HasMany(p => p.WasteLossRecordPackaging)
                .WithOne(wlrp => wlrp.Packaging)
                .HasForeignKey(wlrp => wlrp.PackagingId);

            // WasteLossRecordIngredients
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

            modelBuilder.Entity<Batch>()
            .HasMany(b => b.BatchFinishedProducts)
            .WithOne(bfp => bfp.Batch)
            .HasForeignKey(bfp => bfp.BatchID);

            modelBuilder.Entity<FinishedProduct>()
            .HasMany(fp => fp.BatchFinishedProducts)
            .WithOne(bfp => bfp.FinishedProduct)
            .HasForeignKey(bfp => bfp.ProductID);

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


            modelBuilder.Entity<FinishedProductPackaging>()
            .HasKey(fpp => new { fpp.ProductID, fpp.PackagingID });

            modelBuilder.Entity<FinishedProductPackaging>()
            .HasOne(fpp => fpp.FinishedProduct)
            .WithMany(fp => fp.FinishedProductPackaging)
            .HasForeignKey(fpp => fpp.ProductID);

            modelBuilder.Entity<FinishedProductPackaging>()
            .HasOne(fpp => fpp.Packaging)
            .WithMany(p => p.FinishedProductPackaging)
            .HasForeignKey(fpp => fpp.PackagingID);

            modelBuilder.Entity<Supplier>()
               .HasKey(s => s.SupplierID);
               
            modelBuilder.Entity<Warehouse>()
               .HasOne(w => w.AssignedManager)
               .WithMany()
               .HasForeignKey(w => w.AssignedManagerUserId)
               .OnDelete(DeleteBehavior.SetNull);

        }


    }
}
