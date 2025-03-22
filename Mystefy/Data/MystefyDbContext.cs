using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Models;

namespace Mystefy.Data;

public class MystefyDbContext : DbContext
{
    public MystefyDbContext(DbContextOptions<MystefyDbContext> options) : base(options) {}
    public DbSet<Packaging> Packaging { get; set; }
    public DbSet<User> Users { get; set; } // Added Users table

}
