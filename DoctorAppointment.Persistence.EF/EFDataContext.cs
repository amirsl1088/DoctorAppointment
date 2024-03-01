using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Entities.Receipts;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistence.EF;

public class EFDataContext : DbContext
{
    public EFDataContext(string connectionString) :
        this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
    { }

     
    public EFDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Receipt> Receipts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly
            (typeof(EFDataContext).Assembly);
    }
}