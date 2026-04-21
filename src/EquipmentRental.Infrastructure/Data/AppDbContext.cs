using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EquipmentRental.Domain.entities;

namespace EquipmentRental.Infrastructure.Data;


public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Equipment>( e =>
        {
            e.HasKey(e => e.EquipmentId);
            e.Property(e => e.Name).IsRequired().HasMaxLength(100);
            e.Property(e => e.Category).IsRequired().HasMaxLength(100);
            e.Property(e => e.DailyRate).HasColumnType("decimal(18,2)");


        
        });
        modelBuilder.Entity<Customer>( c =>
        {
            c.HasKey(c => c.CustomerId);
            c.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            c.Property(c => c.LastName).IsRequired().HasMaxLength(100);
            c.Property(c => c.Email).IsRequired().HasMaxLength(100);
            c.Property(c => c.PhoneNumber).HasMaxLength(20);
        });
    }
}