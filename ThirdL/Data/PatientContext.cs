using Microsoft.EntityFrameworkCore;
using ThirdL.Controllers;
using ThirdL.Models;

namespace ThirdL.Data
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Comment> Comments { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		
        public PatientContext(DbContextOptions<PatientContext> options) :base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasMany<Comment>(c => c.Comments)
                .WithOne(c => c.Patient)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>(patients => { patients.HasIndex(p => p.Login).IsUnique(); });
            
            modelBuilder.Entity<Doctor>()
                .HasMany<Patient>(d => d.Patients)
                .WithOne(p => p.Doctor)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Doctor>(doctor => { doctor.HasIndex(d => d.Login).IsUnique(); });
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234");
            }
        }
        
    }
}