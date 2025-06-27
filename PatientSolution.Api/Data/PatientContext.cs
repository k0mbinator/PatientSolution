using Microsoft.EntityFrameworkCore;

namespace PatientSolution.Api.Models
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
            
        }

        public DbSet<Patient> TodoItems { get; set; } = null!;
    }
}