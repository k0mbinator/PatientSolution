using Microsoft.EntityFrameworkCore;
using PatientSolution.Api.Data;
using PatientSolution.Shared.Models;

namespace PatientSolution.Api.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientContext _context;

        public PatientService(PatientContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            try
            {
                return await _context.Patients.OrderBy(p => p.PatientId).ToListAsync();
            }
            catch (Exception ex)
            {
                // Loggen des Fehlers in die Konsole für Debugging-Zwecke
                await Console.Error.WriteLineAsync($"Fehler beim Laden der Patienten mit EF Core: {ex.Message}");
                // Den Fehler erneut werfen, damit er in der Aufrufkette behandelt werden kann
                throw;
            }
        }
    }
}
