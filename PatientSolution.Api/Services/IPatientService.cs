using PatientSolution.Shared.Models;


namespace PatientSolution.Api.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetPatientsAsync();

    }
}