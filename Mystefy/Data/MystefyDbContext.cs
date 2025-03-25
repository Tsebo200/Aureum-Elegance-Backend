using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Models;

namespace Mystefy.Data;

public class MystefyDbContext : DbContext
{
    public MystefyDbContext(DbContextOptions<MystefyDbContext> options) : base(options) {}
    public DbSet<Packaging> Packaging { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Fragrance> Fragrances { get; set; }
    
    public DbSet<User> Users { get; set; } // Added Users table

    public DbSet<FinishedProduct> FinishedProduct { get; set; } // Added Finished Product

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinishedProduct>()
                .HasOne(fp => fp.Fragrance)
                .WithMany()
                .HasForeignKey(fp => fp.FragranceID);

            modelBuilder.Entity<FinishedProduct>()
                .HasOne(fp => fp.Packaging)
                .WithMany()
                .HasForeignKey(fp => fp.PackagingID);

        }
}
