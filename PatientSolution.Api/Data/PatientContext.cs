using Microsoft.EntityFrameworkCore;
using PatientSolution.Shared.Models;

namespace PatientSolution.Api.Data
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("PATIENTS");

            modelBuilder.Entity<Patient>().Property(p => p.PatientId).HasColumnName("PATIENT_ID");
            modelBuilder.Entity<Patient>().Property(p => p.FirstName).HasColumnName("FIRST_NAME");
            modelBuilder.Entity<Patient>().Property(p => p.LastName).HasColumnName("LAST_NAME");
            modelBuilder.Entity<Patient>().Property(p => p.DateOfBirth).HasColumnName("DATE_OF_BIRTH");
            modelBuilder.Entity<Patient>().Property(p => p.Gender).HasColumnName("GENDER");
            modelBuilder.Entity<Patient>().Property(p => p.AddressStreet).HasColumnName("ADDRESS_STREET");
            modelBuilder.Entity<Patient>().Property(p => p.AddressCity).HasColumnName("ADDRESS_CITY");
            modelBuilder.Entity<Patient>().Property(p => p.AddressState).HasColumnName("ADDRESS_STATE");
            modelBuilder.Entity<Patient>().Property(p => p.AddressZip).HasColumnName("ADDRESS_ZIP");
            modelBuilder.Entity<Patient>().Property(p => p.Hl7MessageType).HasColumnName("HL7_MESSAGE_TYPE");
            modelBuilder.Entity<Patient>().Property(p => p.ReceivedDate).HasColumnName("RECEIVED_DATE");

            modelBuilder.Entity<Patient>().HasKey(p => p.PatientId);
        }
    }
}
