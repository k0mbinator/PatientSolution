using Microsoft.AspNetCore.Mvc;
using PatientSolution.Api.Services;
using PatientSolution.Shared.Models;

namespace PatientSolution.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Get()
        {
            try
            {
                var patients = await _patientService.GetPatientsAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                // Log the error to the server console for debugging purposes.
                await Console.Error.WriteLineAsync($"Fehler beim Abrufen der Patienten: {ex.Message}");
                // Return a generic internal server error to the client.
                return StatusCode(500, "Interner Serverfehler beim Abrufen der Patientendaten.");
            }
        }
    }
}
