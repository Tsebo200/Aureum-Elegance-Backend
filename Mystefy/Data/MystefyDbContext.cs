using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Models;

namespace Mystefy.Data;

public class MystefyDbContext : DbContext
{
    public MystefyDbContext(DbContextOptions<MystefyDbContext> options) : base(options) {}
    public DbSet<Packaging> Packaging { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseStock> WarehouseStocksarehouseStocks { get; set; }
    public DbSet<Fragrance> Fragrances { get; set; }
    public DbSet<FragranceIngredient> FragranceIngredients { get; set; }
}
