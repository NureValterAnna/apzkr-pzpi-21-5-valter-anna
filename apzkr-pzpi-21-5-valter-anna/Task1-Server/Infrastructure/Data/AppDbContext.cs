using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Medicine> Medicines { get; set; }

    public DbSet<Prescription> Prescriptions { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<MedicineStock> MedicineStocks { get; set; }

    public DbSet<Dispenser> Dispensers { get; set; }
}
